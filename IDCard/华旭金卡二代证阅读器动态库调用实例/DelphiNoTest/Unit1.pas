unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;
const
   PORT =1001 ; //通讯端口号  1001表示USB1,1表示COM1
   IFOpen=1;
type
  TForm1 = class(TForm)
    Button1: TButton;
    Memo1: TMemo;
    Button2: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
 //静态调用动态库        
  function SDT_StartFindIDCard(iPort:integer;pucIIN:Pbytearray;iIfOpen:integer):integer;stdcall;external 'SDTAPI.DLL' name 'SDT_StartFindIDCard';
  function SDT_SelectIDCard(iPort:integer;pucSN:Pbytearray;iIfOpen:integer):integer;stdcall;external 'SDTAPI.DLL' name 'SDT_SelectIDCard';
  function SDT_ReadBaseMsg(iPort:integer;pucCHMsg:Pbytearray;puiCHMsgLen:PInteger;pucPHMsg:Pbytearray;puiPHMsgLen:PInteger;iIfOpen:integer):integer;stdcall;external 'SDTAPI.DLL' name 'SDT_ReadBaseMsg';

implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var
  iResultValue:Integer;
  cModleSN: Array [0..255]of Char;//安全模块序列号
  CardPUCIIN: array[0..255] of Byte;
  CardPUCSN: array[0..255] of Byte;
  pucCHMsg: array[0..256] of byte;
  pucPHMsg: array[0..1024] of byte;
  pucNewAddInfo: array[0..256] of byte;
  CardCHMsgLen: integer;
  CardPHMsgLen: integer;
begin

  //开始找卡:
  Memo1.Lines.Add('=============================');
  iResultValue := SDT_StartFindIDCard(PORT, @CardPUCIIN, IFOpen);
  If iResultValue = $80 Then  //找卡不成功，可能是没有卡或者卡片一直处于磁场中
  begin
    Memo1.Lines.Add('找卡失败！') ;
    Sleep(300);
    exit;
  end
  else if iResultValue=$2 then
  begin
    Memo1.Lines.Add('通讯超时,请检查设备！') ;
    exit;
  end
  else if iResultValue = $9f Then
  begin
    iResultValue := SDT_SelectIDCard(PORT, @CardPUCSN, IFOpen);
    //读卡片中的标识
    If iResultValue = 144 Then
    begin
      //开始读卡内人员信息
      iResultValue := SDT_ReadBaseMsg(PORT, @pucchmsg, @CardCHMsgLen, @pucphmsg, @CardPHMsgLen, IFOpen);
      if iResultValue <> 144 Then
      begin
        Memo1.Lines.Add( '读卡失败，请再次放卡重试');
        exit;
      end
      else   //处理读到的信息，显示在界面上
      begin
        Memo1.Lines.Add(WideCharLenToString(@pucchmsg,CardCHMsgLen))  ;
      end;
  end
  else
  begin
    Memo1.Lines.Add( '读卡失败，请再次放卡重试');
    Exit;
  end;   
end;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  Close;
end;

end.

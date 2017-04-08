// VCNoTestDlg.cpp : implementation file
//

#include "stdafx.h"
#include "VCNoTest.h"
#include "VCNoTestDlg.h"
#include "NO.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CVCNoTestDlg dialog

CVCNoTestDlg::CVCNoTestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CVCNoTestDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CVCNoTestDlg)
	m_Name = _T("");
	m_Sex = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CVCNoTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CVCNoTestDlg)
	DDX_Text(pDX, IDC_EDIT1, m_Name);
	DDX_Text(pDX, IDC_EDIT2, m_Sex);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CVCNoTestDlg, CDialog)
	//{{AFX_MSG_MAP(CVCNoTestDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CVCNoTestDlg message handlers

BOOL CVCNoTestDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CVCNoTestDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CVCNoTestDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CVCNoTestDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CVCNoTestDlg::OnButton1() 
{
	// TODO: Add your control notification handler code here
	//定义变量
	unsigned char pucManaInfo[255];
	unsigned char pucManaMsg[255];	
	unsigned char  pucCHMsg[255];
	unsigned int  puiCHMsgLen;
	unsigned char  pucPHMsg[1024];
	unsigned int  puiPHMsgLen;
	int iPort=1;
	int st=0;
	//读卡
	st=SDT_StartFindIDCard(iPort,pucManaInfo,1);
	if(st!=0x9f) return ;
	st=SDT_SelectIDCard(iPort,pucManaMsg,1);
    if(st!=0x90) return ;
	st=SDT_ReadBaseMsg(iPort,pucCHMsg,&puiCHMsgLen,pucPHMsg,&puiPHMsgLen,1);
	if(st!=0x90) return ;
	//解析结果
	unsigned short szName[16];
	unsigned short szSex[2];
	memcpy(szName,pucCHMsg,30);
	memcpy(szSex,pucCHMsg+30,2);
	//显示结果
	szSex[1]=0;
	szName[15]=0;
	::SetDlgItemTextW(this->m_hWnd,IDC_EDIT1,szName);
	::SetDlgItemTextW(this->m_hWnd,IDC_EDIT2,szSex);

}

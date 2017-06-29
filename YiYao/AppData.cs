using CardReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace YiYao
{
    public class AppData
    {
        public static IDCard CurrentIDCard;

        public static BitmapSource GetAvatar() {

            BitmapImage Avatar = new BitmapImage();

            var card = AppData.CurrentIDCard;

            if (null == card.HeadImage)
            {
                /***判断是否在根目录下有缓存信息***/
                /***如果没有，则使用默认图片***/
                String userAvatarPath;

                string userAvatar = "/" + card.Name + ".bmp";

                userAvatarPath = Environment.CurrentDirectory + userAvatar;

                bool imageExist;

                imageExist = System.IO.File.Exists(userAvatarPath);

                if (true == imageExist)
                {
                    Avatar = GetBitmapImage(userAvatarPath);
                }
                else
                {
                    string userDefaultHeadImage = card.Sex == "男" ? "/man.png" : "/woman.png";
                    userAvatarPath = Environment.CurrentDirectory + userDefaultHeadImage;
                    Avatar = GetBitmapImage(userAvatarPath);
                }

                card.HeadImage = Avatar;
            }
            else
            {
                return card.HeadImage;
            }
            return Avatar;
        }

       

        public static BitmapImage GetBitmapImage(String imageEnvironmentPath)
        {
            BitmapImage resultValue = new BitmapImage();
            try
            {   
                Uri imageUri = null;

                imageUri = new Uri(imageEnvironmentPath, UriKind.Absolute);
                
                resultValue.BeginInit();

                resultValue.UriSource = imageUri;

                resultValue.EndInit();

                return resultValue;
            }
            catch (Exception)
            {

            }
            return resultValue;
        }
    }
}

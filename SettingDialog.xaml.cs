using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CoreTweet;

namespace GrandBluePairHuntingSupportTool
{
    /// <summary>
    /// SettingDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingDialog : Window
    {

        OAuth.OAuthSession session;
        public Tokens tokens;

        public SettingDialog()
        {
            InitializeComponent();
        }

        private void Button_Twitter_OAuth_Click(object sender, RoutedEventArgs e)
        {
            //Pinの発行
            session = OAuth.Authorize("ConsumerKeyの値", "ConsumerSecretの値");
            System.Diagnostics.Process.Start(session.AuthorizeUri.AbsoluteUri);
        }

        private void Button_Twitter_Pin_Click(object sender, RoutedEventArgs e)
        {
            //トークンの発行
            string pincode = TextBoxPin.Text;
            tokens = OAuth.GetTokens(session, pincode);

            //アクセストークンを設定ファイルに保存
            Properties.Settings.Default.AccessToken = tokens.AccessToken;
            Properties.Settings.Default.AccessTokenSecret = tokens.AccessTokenSecret;
            Properties.Settings.Default.Save();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //監視ユーザのIDを設定ファイルに保存
            Properties.Settings.Default.MonitorId = TextBoxMonitorId.Text;
            Properties.Settings.Default.Save();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            App.Current.MainWindow.Show();
        }
    }
}

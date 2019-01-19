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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoreTweet;

namespace GrandBluePairHuntingSupportTool
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public Tokens tokens;

        public MainWindow()
        {
            InitializeComponent();

            // 設定読み込み
            String ConsumerKey = TwitterKeys.ConsumerKey;
            String ConsumerSecret = TwitterKeys.ConsumerSecret;
            String AccessToken = Properties.Settings.Default.AccessToken;
            String AccessTokenSecret = Properties.Settings.Default.AccessTokenSecret;

            // アクセストークンが記録されていない場合に設定ウィンドウを開く
            if( (AccessToken == "") || (AccessTokenSecret == ""))
            {
                // ウィンドウ生成
                var window = new SettingDialog();
                // ウィンドウ表示
                window.Show();
                // 自身（MainWindow）を隠す
                this.Hide();
            }

            //Twitterに接続
            tokens = Tokens.Create(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

        }

        private void Button_Pick_RescueID_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Open_SettingDialog_Click(object sender, RoutedEventArgs e)
        {
            // ウィンドウ生成
            var window = new SettingDialog();
            // ウィンドウ表示
            window.Show();
            // 自身（MainWindow）を隠す
            this.Hide();
        }

    }
}

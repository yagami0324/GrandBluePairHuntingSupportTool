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
using System.Text.RegularExpressions;

namespace GrandBluePairHuntingSupportTool
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {

        public Tokens tokens;
        public Dictionary<string, object> search_param = new Dictionary<string, object>();

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

            // Twitterに接続
            tokens = Tokens.Create(ConsumerKey, ConsumerSecret, AccessToken, AccessTokenSecret);

            // ツイート取得用パラメータの設定
            search_param["count"] = 1;
            search_param["screen_name"] = Properties.Settings.Default.MonitorId;

        }

        private void Button_Pick_RescueID_Click(object sender, RoutedEventArgs e)
        {
            // 監視ユーザの最新ツイートを取得
            var tweets = tokens.Statuses.UserTimeline(search_param);

            // 正規表現で救援ID・Lv・ボス名を抽出
            string input = tweets[0].Text;
            string expression = @"(?<ID>\w+) :参戦ID\s参加者募集！\sLv(?<LV>[0-9]+) (?<BOSS>.+)\s";
            string rescue_id = "";
            string boss_level = "";
            string boss_name = "";

            Regex reg = new Regex(expression);
            Match match = reg.Match(input);
            rescue_id = match.Groups["ID"].Value;
            boss_level = match.Groups["LV"].Value;
            boss_name = match.Groups["BOSS"].Value;

            // クリップボードに救援IDをコピー
            Clipboard.SetText(rescue_id);

            // TextBlockにツイート情報を表示
            TextBlock_Tweet.Text = rescue_id+" Lv"+boss_level+" "+boss_name;
                        
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

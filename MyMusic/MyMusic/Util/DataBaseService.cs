using MyMusic.Model;
using SQLitePCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusic.Util
{
    public class DataBaseService
    {
        private static DataBaseService instance;
        private static object _lock = new object();
        private SQLiteConnection conn;

        //一些需要的SQL语句——PlayList
        private String SQL_REMOVE_PLAYLIST = "DELETE FROM PLAYLIST" + " WHERE Id = ?";
        private String SQL_INSERT_PLAYLIST = "INSERT INTO PLAYLIST (Id,PicUrl,Url,Name,Artist,Duration,Like,Playing,AlName,Lyric)" + " VALUES(?,?,?,?,?,?,?,?,?,?);";
        private String SQL_REMOVEALL_PLAYLIST = "DELETE * FROM PLAYLIST";
        private String SQL_SELECTALL_PLAYLIST = "SELECT * FROM PLAYLIST";
        private String SQL_UPDATEL_PLAYLIST = "UPDATE PLAYLIST" + " SET Url = ? WHERE ID = ?";
        //LikeSong
        private String SQL_INSERT_LIKESONGS = "INSERT INTO LIKESONGS (Id,PicUrl,Url,Name,Artist,Duration,AlName,Lyric)" + " VALUES(?,?,?,?,?,?,?,?);";
        private String SQL_SELECTALL_LIKESONGS = "SELECT * FROM LIKESONGS";
        private String SQL_REMOVE_LIKESONGS = "DELETE FROM LIKESONGS" + " WHERE Id = ?";
        // Search History
        private String SQL_INSERT_SEARCHHISTORY = "INSERT INTO SEARCHHISTORY (Data) " + " VALUES(?);";
        private String SQL_REMOVE_SEARCHHISTORY = "DELETE FROM SEARCHHISTORY" + " WHERE Data = ?";
        private String SQL_SELECTALL_SEARCHHISTORY = "SELECT * FROM SEARCHHISTORY";
        private String SQL_REMOVEALL_SEARCHHISTORY = "DELETE * FROM SEARCHHISTORY";

        //单例模式
        private DataBaseService()
        {
            //建立连接，如果MySQLite.db不存在，则会新建MySQLite.db
            conn = new SQLiteConnection("MyMusic.db");
            InitializePlaySongsList();
            InitializeLikeSongsList();
            InitializeSearchHistory();
            /*
             *下面这个方法是用来测试PlayingBar控件的，因为一开始还没做好歌曲搜索页面，因此没法添加歌曲到播放列表，所以这里先代码添加歌曲到播放列表
             * 等歌曲播放列表的功能做好了后可以注释掉这部分的测试代码
             */
            TestPlayList();
        }

        public static DataBaseService GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new DataBaseService();
                    }
                }
            }
            return instance;
        }


        private void InitializePlaySongsList()
        {
            //获取表，如果Items表不存在，则新建表Items
            string sql = @"CREATE TABLE IF NOT EXISTS
                                PLAYLIST(
                                      Id VARCHAR(50) PRIMARY KEY NOT NULL,
                                      PicUrl VARCHAR(600) NOT NULL,
                                      Url VARCHAR(600),
                                      Name VARCHAR(600) NOT NULL,
                                      Artist VARCHAR(200) NOT NULL,
                                      Duration VARCHAR(50) NOT NULL,
                                      Like INTEGER NOT NULL,
                                      Playing INTEGER NOT NULL,
                                      AlName VARCHAR(200) NOT NULL,
                                      Lyric VARCHAR(5000) NOT NULL
                                      );";

            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }

        private void TestPlayList()
        {
            using (var statement = conn.Prepare(SQL_SELECTALL_PLAYLIST))
            {
                int count = 0;
                //statement.Step()将依次获得每一行的结果
                while (SQLiteResult.ROW == statement.Step())
                {
                    count++;
                }
                if (count == 0)
                {
                    /*
                    CreatePlayListItem("480908857", "http://p1.music.126.net/KxiVPNes0U4mbGe6zXOipQ==/109951162935943532.jpg",
                        " ",
                        "种族假象（人声本家）", "漆柚/绾绾Akari/纸马/柚子茶",269482, 0, 1);
                    CreatePlayListItem("101355", "http://p1.music.126.net/vMB8UfCpL7mEzRbcUbyI_g==/27487790706991.jpg",
                        " ",
                        "Hero", "韩庚", 247667, 1, 0);
                     */
                    CreatePlayListItem("480908857", "http://p1.music.126.net/KxiVPNes0U4mbGe6zXOipQ==/109951162935943532.jpg"," ","种族假象（人声本家）", "漆柚/绾绾Akari/纸马/柚子茶", 269482, 0, 1, "【踏云社/四假象系列曲】种族假象", "[00:00.00]种族假象\n[00:00.00]漆柚：\n[00:01.79]高坐看台的宿命暗自布下圈套\n[00:05.51]按部就班的出演难免沦为笑料\n[00:09.20]剧中人手舞足蹈可怜又可笑\n[00:12.34]被蛇吞噬殆尽的笼中鸟 徒剩羽毛\n[00:16.72]往复之间深陷泥淖\n[00:21.88]策划 / 混音 / 作曲：Fran\n[00:22.73]编曲：Mr.Beep / fran\n[00:23.50]填词 / 文案：戏中人\n[00:24.48]小提琴实录：绾绾Akari\n[00:25.42]演唱：漆柚\n[00:26.31]feat：绾绾Akari / 纸马 / 柚子茶\n[00:27.16] * *************\n[00:28.17]Scarlet桑戎（乐正绫）\n[00:29.11]芹菜猪肉大馄饨（星尘）\n[00:30.00]影随龙风（心华）\n[00:30.90]画师: SOSO\n[00:31.91]PV：Bung Kon\n[00:36.22]漆柚：\n[00:36.55]故事书中的童话\n[00:38.51]如何分辨真与假\n[00:40.27]绾绾：\n[00:40.38]八音盒里旋转的木马\n[00:42.10]一圈一圈在原地挣扎\n[00:43.84]漆柚：\n[00:43.96]手中这朵月季花\n[00:45.53]真的是红色的吗\n[00:47.47]纸马：\n[00:47.59]眼见为实的谎话\n[00:49.43]何时才会被拆穿呐\n[00:51.18]漆柚（choir绾绾）:\n[00:51.31]理性演绎与归纳\n[00:53.11]感觉做着加减法\n[00:54.86]柚子茶（choir纸马）：\n[00:54.97]这个世界没那么复杂\n[00:56.78]寥寥数笔便得以勾画\n[00:58.57]漆柚（choir绾绾 / 纸马 / 柚子茶）:\n[00:58.68]可惜象牙的高塔\n[01:00.17]下个瞬间便崩塌\n[01:02.20]绾绾 / 纸马 / 柚子茶:\n[01:02.31]那完美的杰作呀\n[01:03.74]破碎得稀里哗啦\n[01:05.85]漆柚：\n[01:05.98]高坐看台的宿命暗自布下圈套（choir纸马）\n[01:09.54]按部就班的出演难免沦为笑料（choir绾绾）\n[01:13.24]剧中人手舞足蹈可怜又可笑（choir纸马）\n[01:16.48]被蛇吞噬殆尽的笼中鸟（choir绾绾） 徒剩羽毛\n[01:20.79]往复之间深陷泥淖（choir柚子茶）\n[01:24.15]漆柚：\n[01:24.26]恪守规则已成为（choir纸马）骨髓深处记号（choir绾绾）\n[01:27.84]就连质疑（choir柚子茶）也都会变得莫名其妙（choir纸马）\n[01:31.52]局中人饮水思饱→（choir绾绾）被逼入死角\n[01:34.73]隔绝（choir柚子茶）于世围困封闭孤岛（choir纸马） 画地为牢\n[01:39.02]囹圄之中在劫难逃(choir绾绾 / 纸马 / 柚子茶)\n[01:56.58]漆柚（choir柚子茶）：\n[01:57.07]闭上眼不看悬崖\n[01:58.96]然后一纵身跳下\n[02:00.74]绾绾（choir纸马 / 柚子茶）:\n[02:00.86]奈何自欺总归是虚假\n[02:02.71]白骨生花尸骸都风化\n[02:04.55]漆柚（choir柚子茶）:\n[02:04.66]心思算尽的谋划\n[02:06.12]都被轻易地抹杀\n[02:08.21]纸马（choir绾绾）:\n[02:08.31]所有坚持像笑话\n[02:09.83]到头来自顾不暇\n[02:11.84]漆柚（choir纸马 / 绾绾）:\n[02:11.95]这信仰有意义吗\n[02:13.37]心中困惑都发芽\n[02:15.51]柚子茶（choir绾绾）:\n[02:15.68]聆听神谕得不到回答\n[02:17.38]约伯也未将上帝抛下\n[02:19.16]漆柚（choir纸马）:\n[02:19.27]明目张胆的谎话\n[02:20.77]就当做不存在吧\n[02:22.80]绾绾 / 纸马 / 柚子茶:\n[02:22.92]〖这故事完美无瑕\n[02:24.44]又有谁能否定它〗\n[02:26.43]漆柚：\n[02:26.53]高坐看台的宿命暗自布下圈套（choir纸马）\n[02:30.12]按部就班的出演难免沦为笑料（choir绾绾）\n[02:33.85]剧中人手舞足蹈可怜又可笑（choir纸马）\n[02:37.07]被蛇吞噬殆尽的笼中鸟 徒剩羽毛\n[02:41.28]往复之间深陷泥淖（choir纸马）\n[02:44.83]漆柚：\n[02:44.94]恪守规则已成为（choir纸马）骨髓深处记号（choir绾绾）\n[02:48.49]就连质疑（choir柚子茶）也都会变得莫名其妙（choir纸马）\n[02:52.13]局中人饮水思饱（choir绾绾）被逼入死角\n[02:55.36]隔绝（choir柚子茶）于世围困封闭孤岛（choir纸马） 画地为牢\n[02:59.59]囹圄之中在劫难逃(choir绾绾 / 纸马 / 柚子茶)\n[03:03.47]（小提琴solo By 绾绾Akari）\n[03:34.28]漆柚：\n[03:34.43]高坐看台的宿命暗自布下圈套\n[03:37.94]按部就班的出演难免沦为笑料\n[03:41.55]剧中人手舞足蹈可怜又可笑\n[03:44.81]被蛇吞噬殆尽的笼中鸟 徒剩羽毛\n[03:49.14]往复之间深陷泥淖\n[03:52.56]漆柚：\n[03:52.67]恪守规则已成为（choir纸马）骨髓深处记号（choir绾绾）\n[03:56.25]就连质疑（choir柚子茶）也都会变得莫名其妙（choir纸马）\n[03:59.86]局中人饮水思饱（choir绾绾）被逼入死角\n[04:03.13]隔绝（choir柚子茶）于世围困封闭孤岛（choir纸马） 画地为牢\n[04:07.52]囹圄之中在劫难逃(choir绾绾/纸马/柚子茶)\n");
                    CreatePlayListItem("29822196", "http://p1.music.126.net/Bnd2VwFCu8nWWZ8DY5BWcg==/3294136838291285.jpg", " ", "海绵宝宝片尾曲", "Various Artists", 46759, 0, 1, "最新热歌慢摇90"," ");
                    CreatePlayListItem("28625030", "http://p1.music.126.net/fIv14Wd9nlpmAoUqPGEhAA==/3405187512278988.jpg", " ", "香蕉之歌", "小黄人", 49000, 0, 0, "最新热歌慢摇65"," ");
                    CreatePlayListItem("26548604","http://p1.music.126.net/Xuk6VKayo-IDhSvtMtj2Gw==/4464017208779307.jpg"," ","Ba Do Bleep (Bonus Track)","The Minions",14173,0,0, "Despicable Me 2 (Original Motion Picture Soundtrack)", " ");
                    CreatePlayListItem("30148444", "http://p1.music.126.net/claKS0BZfzaTqJAZcQjP5w==/3395291907804742.jpg", " ", "happy tree friends 主题曲", "Various Artists", 28656, 0, 0, "最新热歌慢摇93"," ");
                    CreatePlayListItem("1235689", "http://p1.music.126.net/36H5rmEKgYlP540LbRqYKA==/778454242745262.jpg", " ", "Opening Titles", "David Arnold", 40213, 0, 0, "Sherlock: Music from Series 1 (Original Television Soundtrack)"," ");
                }
            }
        }

        internal void CreatePlayListItem(PlayingSongForSqlModel model)
        {
            using (var statement = conn.Prepare(SQL_INSERT_PLAYLIST))
            {
                //这里的Bind会依次替换掉上面语句中的问号
                statement.Bind(1, model.Id);
                statement.Bind(2, model.PicUrl);
                statement.Bind(3, model.Url);
                statement.Bind(4, model.Name);
                statement.Bind(5, model.Artist);
                statement.Bind(6, model.Duration.ToString());
                statement.Bind(7, model.Like);
                statement.Bind(8, model.Playing);
                statement.Bind(9, model.AlName);
                statement.Bind(10, model.Lyric);
                statement.Step();
            }
        }

        public void CreatePlayListItem(string id, string picUrl, string url, string name, string artist, long duration, int like,int playing,string alNmae,string lyric)
        {
            using (var statement = conn.Prepare(SQL_INSERT_PLAYLIST))
            {
                //这里的Bind会依次替换掉上面语句中的问号
                statement.Bind(1, id);
                statement.Bind(2, picUrl);
                statement.Bind(3, url);
                statement.Bind(4, name);
                statement.Bind(5, artist);
                statement.Bind(6, duration.ToString());
                statement.Bind(7, 0);
                statement.Bind(8, playing);
                statement.Bind(9, alNmae);
                statement.Bind(10, lyric);
                statement.Step();
            }
        }

        public void GetAllPlayListItem(ObservableCollection<PlayingSongForSqlModel> playingList)
        {
            using (var statement = conn.Prepare(SQL_SELECTALL_PLAYLIST))
            {
                //statement.Step()将依次获得每一行的结果
                while (SQLiteResult.ROW == statement.Step())
                {
                    //(string id, string picUrl, string url, string name, string artist, double duration, bool like)
                    //int id = int.Parse(statement[0].ToString());
                    string id = (string)statement[0];
                    string picUrl = (string)statement[1];
                    string url = (string)statement[2];
                    string name = (string)statement[3];
                    string artist = (string)statement[4];
                    long duration = long.Parse(statement[5].ToString());
                    int like = int.Parse(statement[6].ToString());
                    int playing = int.Parse(statement[7].ToString());
                    string alName = (string)statement[8];
                    string lyric = (string)statement[9];
                    playingList.Add(new PlayingSongForSqlModel(id, picUrl, url, name, artist, duration, like,playing,alName,lyric));
                }
            }
        }

        public void UpdatePlayList(string id,string url)
        {
            using (var statement = conn.Prepare(SQL_UPDATEL_PLAYLIST))
            {
                statement.Bind(1, url);
                statement.Bind(2, id);
                statement.Step();
            }
        }



        // 下面是对数据库的另一张表的操作
        private void InitializeLikeSongsList()
        {
            //获取表，如果Items表不存在，则新建表Items
            string sql = @"CREATE TABLE IF NOT EXISTS
                                LIKESONGS(
                                      Id VARCHAR(50) PRIMARY KEY NOT NULL,
                                      PicUrl VARCHAR(600) NOT NULL,
                                      Url VARCHAR(600),
                                      Name VARCHAR(600) NOT NULL,
                                      Artist VARCHAR(200) NOT NULL,
                                      Duration VARCHAR(50) NOT NULL,
                                      AlName VARCHAR(200) NOT NULL,
                                      Lyric VARCHAR(5000) NOT NULL
                                      );";

            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }


        public void CreateLikeSongs(string id,string picUrl,string url,string name,string artist,double duration,string alName,string lyric)
        {
            using (var statement = conn.Prepare(SQL_INSERT_LIKESONGS))
            {
                //这里的Bind会依次替换掉上面语句中的问号
                statement.Bind(1, id);
                statement.Bind(2, picUrl);
                statement.Bind(3, url);
                statement.Bind(4, name);
                statement.Bind(5, artist);
                statement.Bind(6, duration.ToString());
                statement.Bind(7, alName);
                statement.Bind(8, lyric);
                statement.Step();
            }
        }

        public void GetAllLikeSongsItem(ObservableCollection<LikeSongModelForSql> playingList)
        {
            using (var statement = conn.Prepare(SQL_SELECTALL_LIKESONGS))
            {
                //statement.Step()将依次获得每一行的结果
                while (SQLiteResult.ROW == statement.Step())
                {
                    //(string id, string picUrl, string url, string name, string artist, double duration, bool like)
                    //int id = int.Parse(statement[0].ToString());
                    string id = (string)statement[0];
                    string picUrl = (string)statement[1];
                    string url = (string)statement[2];
                    string name = (string)statement[3];
                    string artist = (string)statement[4];
                    long duration = long.Parse(statement[5].ToString());
                    string alName = (string)statement[6];
                    string lyric = (string)statement[7];
                    playingList.Add(new LikeSongModelForSql(id, picUrl, url, name, artist, duration, alName, lyric));
                }
            }
        }

        internal void RemoveLikeSongs(string id)
        {
            using (var statement = conn.Prepare(SQL_REMOVE_LIKESONGS))
            {
                statement.Bind(1, id);
                statement.Step();
            }
        }


        private void InitializeSearchHistory()
        {
            //获取表，如果Items表不存在，则新建表Items
            string sql = @"CREATE TABLE IF NOT EXISTS
                                SEARCHHISTORY(
                                      Data VARCHAR(200) PRIMARY KEY NOT NULL
                                      );";

            using (var statement = conn.Prepare(sql))
            {
                statement.Step();
            }
        }

        public void GetAllSearchHistory(ObservableCollection<String> history)
        {
            using (var statement = conn.Prepare(SQL_SELECTALL_SEARCHHISTORY))
            {
                //statement.Step()将依次获得每一行的结果
                while (SQLiteResult.ROW == statement.Step())
                {
                    string data = (string)statement[0];
                    history.Add(data);
                }
            }
        }

        public void CreateHistory(string history)
        {
            using (var statement = conn.Prepare(SQL_INSERT_SEARCHHISTORY))
            {
                statement.Bind(1, history);
                statement.Step();
            }
        }

        public void RemoveHistory(string history)
        {
            using (var statement = conn.Prepare(SQL_REMOVE_SEARCHHISTORY))
            {
                statement.Bind(1, history);
                statement.Step();
            }
        }

        public void RemoveAllHistory()
        {
            using (var statement = conn.Prepare(SQL_REMOVEALL_SEARCHHISTORY))
            {
                statement.Step();
            }
        }



    }
}

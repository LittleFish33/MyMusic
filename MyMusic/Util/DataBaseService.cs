using MyMusic.Model;
using SQLitePCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private String SQL_INSERT_PLAYLIST = "INSERT INTO PLAYLIST (Id,PicUrl,Url,Name,Artist,Duration,Like,Playing)" + " VALUES(?,?,?,?,?,?,?,?);";
        private String SQL_REMOVEALL_PLAYLIST = "DELETE * FROM PLAYLIST";
        private String SQL_SELECTALL_PLAYLIST = "SELECT * FROM PLAYLIST";
        private String SQL_UPDATEL_PLAYLIST = "UPDATE PLAYLIST" + " SET Url = ? WHERE ID = ?";


        //单例模式
        private DataBaseService()
        {
            //建立连接，如果MySQLite.db不存在，则会新建MySQLite.db
            conn = new SQLiteConnection("MyMusic.db");
            InitializePlayList();
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


        private void InitializePlayList()
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
                                      Playing INTEGER NOT NULL
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
                    CreatePlayListItem("29822196", "http://p1.music.126.net/Bnd2VwFCu8nWWZ8DY5BWcg==/3294136838291285.jpg", " ", "海绵宝宝片尾曲", "Various Artists", 46759, 0, 1);
                    CreatePlayListItem("28625030", "http://p1.music.126.net/fIv14Wd9nlpmAoUqPGEhAA==/3405187512278988.jpg", " ", "香蕉之歌", "小黄人", 49000, 0, 0);
                    CreatePlayListItem("26548604","http://p1.music.126.net/Xuk6VKayo-IDhSvtMtj2Gw==/4464017208779307.jpg"," ","Ba Do Bleep (Bonus Track)","The Minions",14173,0,0);
                    CreatePlayListItem("30148444", "http://p1.music.126.net/claKS0BZfzaTqJAZcQjP5w==/3395291907804742.jpg", " ", "happy tree friends 主题曲", "Various Artists", 28656, 0, 0);
                    CreatePlayListItem("1235689", "http://p1.music.126.net/36H5rmEKgYlP540LbRqYKA==/778454242745262.jpg", " ", "Opening Titles", "David Arnold", 40213, 0, 0);
                }
            }
        }

        public void CreatePlayListItem(string id, string picUrl, string url, string name, string artist, long duration, int like,int playing)
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
                    playingList.Add(new PlayingSongForSqlModel(id, picUrl, url, name, artist, duration, like,playing));
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

    }
}

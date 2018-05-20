# MyMusic
现代操作系统应用开发期中课程设计
MyMusic 0.5 版本新鲜出炉，人生一大错觉，总觉得项目就快打好了。。。༼ ಥل͟ಥ ༽

首先，首页已经做好了：

![](http://pic.caigoubao.cc/590732/%E5%A4%A7%E4%BA%8C%E4%B8%8B/%E4%BB%99%E8%8D%89/%E6%9C%9F%E4%B8%AD/img1.png)

是不是很帅，，，＼（＠￣∇￣＠）／

首先，侧边栏的响应已经做好了，除了点击**发现音乐**之外，其他的界面都会跳转到BlankPage，你们需要在MainPageViewModel.cs文件中更改为需要跳转到的页面：

![](http://pic.caigoubao.cc/590732/%E5%A4%A7%E4%BA%8C%E4%B8%8B/%E4%BB%99%E8%8D%89/%E6%9C%9F%E4%B8%AD/img2.png)

然后，整个页面的结构主要是一个侧边栏，中间一个ContentFrame，下面一个PlayingBar

然后你们做的基本应该是把你们的页面放在ContentFrame，所以，你们换ContentFrame里的内容为你们的内容的方法：

1. 先获取一个ContentFrameViewModel单例

```c++
private ContentFrameViewModel ContentFrameVm = ContentFrameViewModel.GetInstance();
```

2. 然后添加函数：

```c++
// 这里是点击更多按钮然后进行页面跳转
private void MoreSongSheet_PointerReleased(object sender, PointerRoutedEventArgs e)
{
    ContentFrameVm.NavigateToPage(typeof(BlankPage));
    SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = ContentFrameVm.contentFrameRef.CanGoBack ?
        AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
}
```

***

应该没了吧，懒得写了，关于Json字符串的解析问题：[任意门](http://littlefish33.cn/uwp/JsonParsing/)

And，下面是一些规范：

* 对应文件存放在对应文件夹
* 所有用到的API链接放在Util文件夹里的APIUrlReference.cs文件里
* DataBaseService：数据库操作的类，所有数据库的操作请放在这个类，然后调用这个类的方法
* HttpService：所有网络访问的操作请放在这个类，然后调用这个类的方法（PS：我有一处没用，懒得改了，啦啦啦）

***

下面说一下我已经实现的功能：

* 侧边栏的响应，你们只要设置好应该跳转到哪个页面就行了
* 自适应界面
* 调用api动态生成内容
* 然后暂时播放列表是静态的生成的，能播放5首歌，需要播放界面和搜索界面提供部分功能，等我慢慢来
* 下面的播放器是能用的哦，只剩一个小功能我想等播放列表能动态生成再加上去

***

好的，应该没了，我知道看完好像对你们理解我的项目没有什么卵用，代码有写的很乱，而且还没注释，所有有事就问我吧，但是因为文件很多，一些不影响你们写代码的部分我可能会选择懒得解释，只要能用接口就行了，

๑乛◡乛๑

还有，播放列表的歌只是为了方便测试，所以找了一些比较短的音乐，和我个人的喜好没有任何关系！！！




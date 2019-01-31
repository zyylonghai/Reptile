using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Wesley.Crawler.StrongCrawler.Events;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.UI;
using Wesley.Crawler.StrongCrawler.Models;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Configuration;

namespace Wesley.Crawler.StrongCrawler
{

    public class StrongCrawler : ICrawler
    {
        public event EventHandler<OnStartEventArgs> OnStart;//爬虫启动事件

        public event EventHandler<OnCompletedEventArgs> OnCompleted;//爬虫完成事件

        public event EventHandler<OnErrorEventArgs> OnError;//爬虫出错事件

        public event EventHandler<OnfinallEventArgs> Onfinally;//爬虫完后事件

        private PhantomJSOptions _options;//定义PhantomJS内核参数

        private PhantomJSDriverService _service;//定义Selenium驱动配置

        private static ChromeDriver _chDriver;
        private string[] _Extension;

        public List<ReptileInfo> Data { get; set; }

        public ChromeDriver ChDriver
        {
            get
            {
                ChromeOptions options = new ChromeOptions();
                if (_Extension != null && _Extension.Length > 0 && _chDriver == null)
                {
                    options.AddExtensions(_Extension);
                }
                if (_chDriver == null)
                {
                    _chDriver = new ChromeDriver(options);
                }
                return _chDriver;
            }
            set { _chDriver = value; }
        }

        public StrongCrawler(string proxy = null)
        {
            this._options = new PhantomJSOptions();//定义PhantomJS的参数配置对象
            this._service = PhantomJSDriverService.CreateDefaultService(Environment.CurrentDirectory);//初始化Selenium配置，传入存放phantomjs.exe文件的目录
            _service.IgnoreSslErrors = true;//忽略证书错误
            _service.WebSecurity = false;//禁用网页安全
            _service.HideCommandPromptWindow = true;//隐藏弹出窗口
            _service.LoadImages = false;//禁止加载图片
            _service.LocalToRemoteUrlAccess = true;//允许使用本地资源响应远程 URL
            _options.AddAdditionalCapability(@"phantomjs.page.settings.userAgent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36");
            if (proxy != null)
            {
                _service.ProxyType = "HTTP";//使用HTTP代理
                _service.Proxy = proxy;//代理IP及端口
            }
            else
            {
                _service.ProxyType = "none";//不使用代理
            }
        }

        /// <summary>
        /// 高级爬虫
        /// </summary>
        /// <param name="uri">抓取地址URL</param>
        /// <param name="script">要执行的Javascript脚本代码</param>
        /// <param name="operation">要执行的页面操作</param>
        /// <returns></returns>
        public async Task Start(TastMast mast, Script script, Operation operation)
        {
            Uri uri = new Uri(mast.Url);
            await Task.Run(() =>
            {
                if (OnStart != null) this.OnStart(this, new OnStartEventArgs(uri));
                //var driver = new PhantomJSDriver(_service, _options);//实例化PhantomJS的WebDriver
                ChromeOptions options = new ChromeOptions();
                if (mast.isAMZPRO)
                {
                    this._Extension = new string[1] {
                        string.Format("{0}/njopapoodmifmcogpingplfphojnfeea.crx", Environment.CurrentDirectory) };
                    ChDriver.Navigate().GoToUrl("https://www.amazon.com");
                    try
                    {
                        var watch = DateTime.Now;

                        ChDriver.Navigate().GoToUrl(mast.Url);

                        #region Search Product
                        if (!string.IsNullOrEmpty(mast.SearchInputID))
                        {
                            IWebElement search = ChDriver.FindElementById(mast.SearchInputID);
                            if (search != null)
                            {
                                search.SendKeys(mast.SearchKey);
                            }
                        }
                        if (!string.IsNullOrEmpty(mast.SearchBtnID))
                        {
                            var searchbtn = ChDriver.FindElementByXPath(mast.SearchBtnID);
                            if (searchbtn != null)
                            {
                                searchbtn.Click();
                            }
                            System.Threading.Thread.Sleep(8000);
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(30000);
                        }

                        #endregion

                        #region Open AmazcoutPro
                        //var AMZ = ChDriver.FindElementByXPath("//div[@class='content-wrapper']");
                        IWebElement AMZ = null;
                        if (!string.IsNullOrEmpty(mast.AMZxpath))
                        {
                            AMZ = ChDriver.FindElementByXPath(mast.AMZxpath);
                            //AMZ = ChDriver.FindElementByClassName(mast.AMZxpath);

                        }
                        else
                        {
                            AMZ = ChDriver.FindElementByXPath(ConfigurationManager.AppSettings["AmazcoutPro"].ToString ());
                        }
                        if (AMZ != null)
                        {
                            AMZ.Click();
                        }
                        System.Threading.Thread.Sleep(30000);

                        #endregion


                        #region 登录amzpro插件
                        try
                        {
                            var google = ChDriver.FindElementByXPath(ConfigurationManager.AppSettings["googlelogin"].ToString());
                            if (google != null)
                            {
                                string oldwin = ChDriver.CurrentWindowHandle;
                                google.Click();
                                System.Threading.Thread.Sleep(3000);
                                var pag = ChDriver.SwitchTo().Window(ChDriver.WindowHandles[ChDriver.WindowHandles.Count - 1]);
                                var accoutinput = pag.FindElement(By.Id(ConfigurationManager.AppSettings["accoutinput"].ToString()));
                                if (accoutinput != null)
                                {
                                    accoutinput.SendKeys(ConfigurationManager.AppSettings["accout"].ToString());
                                    System.Threading.Thread.Sleep(1000);
                                    var next = pag.FindElement(By.XPath(ConfigurationManager.AppSettings["next"].ToString()));
                                    if (next != null)
                                    {
                                        next.Click();
                                        System.Threading.Thread.Sleep(3000);
                                        var pwd = pag.FindElement(By.XPath(ConfigurationManager.AppSettings["pwdelem"].ToString()));
                                        if (pwd != null)
                                        {
                                            pwd.SendKeys(ConfigurationManager.AppSettings["pwd"].ToString());
                                            System.Threading.Thread.Sleep(1000);
                                            var next2 = pag.FindElement(By.XPath(ConfigurationManager.AppSettings["next2"].ToString()));
                                            if (next2 != null)
                                            {
                                                next2.Click();
                                                ChDriver.SwitchTo().Window(oldwin);
                                                System.Threading.Thread.Sleep(3000);
                                                try
                                                {
                                                    var telep = pag.FindElement(By.XPath(ConfigurationManager.AppSettings["telepelem"].ToString()));
                                                    if (telep != null)
                                                    {
                                                        telep.SendKeys(ConfigurationManager.AppSettings["telep"].ToString());
                                                        System.Threading.Thread.Sleep(1000);
                                                        var next3 = pag.FindElement(By.XPath(ConfigurationManager.AppSettings["next3"].ToString()));
                                                        if (next3 != null)
                                                        {
                                                            next3.Click();
                                                            System.Threading.Thread.Sleep(3000);
                                                        }
                                                    }
                                                }
                                                catch (Exception excelp)
                                                {
                                                  
                                                }
                                            }
                                        }
                                    }
                                }

                                ChDriver.Navigate().GoToUrl(mast.Url);

                                #region Search Product
                                if (!string.IsNullOrEmpty(mast.SearchInputID))
                                {
                                    IWebElement search = ChDriver.FindElementById(mast.SearchInputID);
                                    if (search != null)
                                    {
                                        search.SendKeys(mast.SearchKey);
                                    }
                                }
                                if (!string.IsNullOrEmpty(mast.SearchBtnID))
                                {
                                    var searchbtn = ChDriver.FindElementByXPath(mast.SearchBtnID);
                                    if (searchbtn != null)
                                    {
                                        searchbtn.Click();
                                    }
                                    System.Threading.Thread.Sleep(8000);
                                }
                                else
                                {
                                    System.Threading.Thread.Sleep(30000);
                                }
                                
                                #endregion

                                #region Open AmazcoutPro
                                IWebElement AMZ2 = null;
                                if (!string.IsNullOrEmpty(mast.AMZxpath))
                                {
                                    AMZ2 = ChDriver.FindElementByXPath(mast.AMZxpath);
                                }
                                else
                                {
                                    AMZ2 = ChDriver.FindElementByXPath(ConfigurationManager.AppSettings["AmazcoutPro"].ToString());
                                }
                                if (AMZ2 != null)
                                {
                                    AMZ2.Click();
                                }
                                System.Threading.Thread.Sleep(30000);
                                #endregion

                            }
                            //var accoutinput = ChDriver.FindElementById("identifierId");
                        }
                        catch (Exception ex)
                        {
                            
                        }

                        #endregion

                        #region old code search product
                        //ChDriver.Navigate ().GoToUrl("https://www.amazon.com/");//请求URL地址
                        //var searchtext = ChDriver.FindElementById("twotabsearchtextbox");
                        //if (searchtext != null)
                        //{
                        //    searchtext.Clear();
                        //    searchtext.SendKeys("四维电子显微镜");
                        //}
                        //var searchbtn = ChDriver.FindElementByXPath("//input[@tabindex='20']");
                        //if (searchbtn != null)
                        //{
                        //    searchbtn.Click();
                        //}
                        #endregion
                        //var amt = ChDriver.FindElementByXPath("//a[@ng-if='p.fbaFees'][@class='ng-binding ng-scope']").Text;

                        //var ssss = ChDriver.FindElementByClassName("maintable");
                        if (script != null) ChDriver.ExecuteScript(script.Code, script.Args);//执行Javascript代码
                        if (operation.Action != null) operation.Action.Invoke(ChDriver);
                        var driverWait = new WebDriverWait(ChDriver, TimeSpan.FromMilliseconds(operation.Timeout));//设置超时时间为x毫秒
                        if (operation.Condition != null) driverWait.Until(operation.Condition);
                        var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;//获取当前任务线程ID
                        var milliseconds = DateTime.Now.Subtract(watch).Milliseconds;//获取请求执行时间;
                        var pageSource = ChDriver.PageSource;//获取网页Dom结构
                        this.OnCompleted?.Invoke(this, new OnCompletedEventArgs(uri, threadId, milliseconds, pageSource, ChDriver));
                    }
                    catch (Exception exc)
                    {
                        this.OnError?.Invoke(this, new OnErrorEventArgs(uri, exc));
                    }
                    finally
                    {
                        this.Onfinally?.Invoke(this, new OnfinallEventArgs());
                    }

                }
                else
                {
                    options.AddExtensions(string.Format("{0}/zhushou_v2.3.0.crx", Environment.CurrentDirectory));

                    var driver = ChDriver;
                    //var driver = new ChromeDriver();
                    try
                    {
                        #region 如果需要登录，需先进行登录

                        if (!string.IsNullOrEmpty(mast.LoginUrl))
                        {
                            driver.Navigate().GoToUrl(mast.LoginUrl);
                            IWebElement userElem = null;
                            try
                            {
                                userElem = driver.FindElementById(mast.UserInputId);
                            }
                            catch (Exception EX2)
                            {
                                userElem = driver.FindElementByName(mast.UserInputId);
                            }
                            if (userElem != null)
                            {
                                userElem.SendKeys(mast.UserID);
                            }
                            IWebElement pwd = null;
                            try
                            {
                                pwd = driver.FindElementById(mast.PwdInputId);
                            }
                            catch (Exception ex3)
                            {
                                pwd = driver.FindElementByName(mast.PwdInputId);
                            }
                            if (pwd != null)
                            {
                                pwd.SendKeys(mast.Pwd);
                            }
                            IWebElement loginbtn = null;
                            try
                            {
                                loginbtn = driver.FindElementById(mast.LoginBtnId);
                            }
                            catch (Exception ex4)
                            {
                                loginbtn = driver.FindElementByName(mast.LoginBtnId);
                            }
                            if (loginbtn != null)
                            {
                                loginbtn.Click();
                            }
                            //driver.FindElementByXPath
                        }
                        #endregion
                        var watch = DateTime.Now;
                        driver.Navigate().GoToUrl(uri.ToString());//请求URL地址
                        if (script != null) driver.ExecuteScript(script.Code, script.Args);//执行Javascript代码
                        if (operation.Action != null) operation.Action.Invoke(driver);
                        var driverWait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(operation.Timeout));//设置超时时间为x毫秒
                        if (operation.Condition != null) driverWait.Until(operation.Condition);
                        var threadId = System.Threading.Thread.CurrentThread.ManagedThreadId;//获取当前任务线程ID
                        var milliseconds = DateTime.Now.Subtract(watch).Milliseconds;//获取请求执行时间;
                        var pageSource = driver.PageSource;//获取网页Dom结构
                        this.OnCompleted?.Invoke(this, new OnCompletedEventArgs(uri, threadId, milliseconds, pageSource, driver));
                    }
                    catch (Exception ex)
                    {
                        this.OnError?.Invoke(this, new OnErrorEventArgs(uri, ex));
                    }
                    finally
                    {
                        this.Onfinally?.Invoke(this, new OnfinallEventArgs());
                        //driver.Close();
                        //driver.Quit();
                    }
                }
            });
        }
    }

}




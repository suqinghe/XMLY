 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *             
 * Copyright (c) 2013-2015 sunliang23456@126.com. All Rights Reserved.                     * 
 *                                                                                         * 
 * Licensed under the GPL or commercial licenses.                                          *                                          
 * To use it on other terms please contact us: sunliang23456@126.com                       *                       
 *                                                                                         * 
 * 可自由使用和复制,但请保留版权信息.                                                      *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 
 SD.Net(Http处理类;适合SDK、网页抓取、模拟请求)
 
 类 说 明：Http请求类【该类有以下特性：
                 支持链式(Fluent API)操作;
				 能够自动处理Cookie(按域名独立或共享Cookies,支持301/302跳转Cookie拦截,支持读取IE浏览器Cookie);
                 支持HTTPS协议;
                 支持证书;
                 支持代理;
                 支持Session会话保持;
                 支持JSON/XML请求;
                 支持JSONP内容自动识别为JSON;
                 支持多文件上传与下载;
                 支持自动GZIP,DEFLATE编码和解码;
                 支持自动解析网页编码;
                 支持响应转为文件、文本、图片、内存流、byte数组、反序列化成实体类;
                 支持Jquery Dom查询(ToDom方法需要引入SD.Net.Html.dll);
             】
 重要提示：请勿自行修改本类，若将无法升级到新版本。
 编码日期：2015-11-28
 编 码 人：孙亮
 联系方式：
         [QQ]:
             978462541
         [Email]:
             sunliang23456@126.com
 版 本 号：1.7.9
 未来计划:
         【待开发】支持文件断点续传
         【待开发】支持异步编程
 修改历史：
		 [2017-07-20]:
			修复Cookie自动存储模块出现重复name的Cookie错误;
			调整HttpClient类，增加多个Create方法重载;
			调整HttpClient类，增加OnRedirect方法用于拦截全局跳转;
			调整HttpClient类，增加LoadIECookie方法重载用于自动加载IE浏览器Cookie到全局容器中;

		 [2017-07-07]:
			调整IHttpResponse.ToType<T>(T typeExample)接口,支持匿名类型,如：
                var d = HttpClient.Create("http://a.com/Api/User/Get/10").ToType(new { Data = new { Name = "" } }).Data.Name;

		 [2017-07-04]:
			调整Json序列化/反序列化，如果项目中有Newtonsoft.Json则自动使用Newtonsoft.Json.dll序列化/反序列化Json结果;
			调整IHttpResponse.ToType<T>()接口,支持dynamic泛型参数,如：
				var d = HttpClient.Create("http://a.com/Api/User/Get/10").ToType<dynamic>().Data.Name;

			修复IHttpRequest.SetHeader设置请求头referer被转义错误;
			修复响应状态(301/302)后跳转地址获取报错问题;
		 [2017-05-16]:
			增强调试时候有效信息显示，隐藏无用字段;
			增加HttpExtensions常用扩展信息;
		 [2017-04-25]:
			增加IHttpResponse.ToType<T>(T exampleType)接口;
		 [2016-12-08]:
			修复HttpException异常中无法获取错误响应的问题;
		 [2016-11-25]:
			调整异常抛出对象,由HttpWebException改成HttpException,可获取更多关于Http信息;
			修复由于Url中出现特殊字符造成Http Head 报错问题;
		 [2016-10-31]:
			修复Cookie设定超过系统默认数量限制，导致Cookie丢失错误;
		 [2016-10-24]:
			修复Cookie设定Domain错误;
		 [2016-10-13]:
			修复IHttpRequest.SetBody传递object类型参数时候请求Body错误问题;
			调整IHttpRequest接口定义，增加SetBody(string)方法;
		 [2016-09-27]:
			修复超时时间设置无效问题;
		 [2016-08-30]:
			调整HttpClient定义，增加Create(string url, Encoding encoding)方法;
		 [2016-08-11]:
			调整IHttpRequest接口定义，增加AutoEncoding属性定义;
			修复获取响应持久化失败的错误;
         [2016-05-27]:
			调整Json序列化中有关日期的格式;
			调整IHttpRequest接口定义，OnRequest、OnResponse增加请求响应过程参数控制，实现重试请求和跳转控制能力;
			调整IHttpRequest接口定义，Clone()增加深度克隆和浅拷贝区分;
			调整IHttpResponse接口定义，增加Locations属性，记录已经301/302跳转的Url集合;
			调整HttpClient定义，增加FindCookie(IHttpRequest ihttpRequest);
			调整IHttpRequest.Go方法,该方法会清空之前保留的Form Data,Url Data,File Data数据;
			修复IHttpRequest.SetBody方法无法正常设定Json/Xml Body错误;
			修复IHttpRequest.GetResponse方法无法正确持久化响应的错误;
         [2016-01-22]:
			调整IHttpResponse接口定义，增加GetHeader(string name)方法，增加ResponseUri，HaveResponse属性;
			调整HttpClient定义，增加LoadIECookie(string host)方法;
         [2016-01-14]:
			修复自动解码失败问题;
         [2015-11-27]:
             调整IHttpRequest接口定义，增加UseGlobalCookies属性，以支持所有实例共享Cookies;
             调整IHttpRequest接口定义，增加AutoRedirect属性，以支持控制是否自动重定向到最新的页面;
             调整IHttpRequest接口定义，增加SetCookie(CookieCollection cookies)方法;
             调整IHttpRequest接口定义，增加SetHeader(WebHeaderCollection headers)方法;
             调整HttpClient，增加OnRequest，OnResponse方法;
             新增CookieStore 管理全局Cookie;
             去除 ActionUrl和BaseUrl ,定义为 Url;
         [2015-10-10]:
             调整IHttpRequest接口定义，增加Clone方法;
         [2015-09-29]:
             调整IHttpRequest接口定义，增加Set,SetHeader,SetCookie方法;
         [2015-09-22]:
             调整IHttpRequest接口定义，增加SetBody,ClearXX,OnRequest,OnResponse,Go方法，去除SetObject方法，调整部分函数定义支持Xml;
         [2015-09-18]:
             增加IHttpResponse接口，扩展类获取文本,File,Stream,Bytes类型的数据的的方法;
         [2015-06-21]:
             基础类构建完成
 示例代码：
         【注】：以下 http://www.demo.com/ 网址需要换成实际网址。
         
         [示例1]:
		 	 /************************************************************************************
			 * 该示例展示了最基本的用法:获取响应文本,对象,图片,文件,字节数组等
			 *************************************************************************************/

             var http = HttpClient.Create();
             var txt1 = http.Go("http://www.demo.com/").GetResponse().ToText();
             var json = http.Go("http://www.demo.com/http/Porudct/1").GetResponse().ToType(typeof(Porudct));
             var img1 = http.Go("http://www.demo.com/img/logo.png").GetResponse().ToImage();
             var bytes= http.Go("http://www.demo.com/img/logo.png").GetResponse().ToBytes();
                        http.Go("http://www.demo.com/img/logo.png").GetResponse().ToFile("C:/1.jpg");
             
         [示例2]:
		 	 /************************************************************************************
			 *	该示例展示了类似于Jquery的Html DOM 操作(ToDom方法需要引入SD.Net.Html.dll);支持原生
			 *  的Css3选择器
			 *************************************************************************************/

             var dom   = HttpClient.Create("https://www.baidu.com/s?wd=a001").ToDom();
             var title = dom.Find("title:first").Text();                                             //title：a_百度搜索
             var list  = dom.Find("#content_left h3.t").Select(p=>p.InnerText).ToList();             //list：所有搜索的链接标题
             var list  = dom.Find("#content_left h3.t").Elements.Select(p => p.InnerText).ToList();  //list：所有搜索的链接标题
             
         [示例3]:
             var login = HttpClient.Create("http://www.demo.com/Auth/Login", "POST")
                .SetParameter(new
                {
                    LoginAccount = "/*账户*/",
                    LoginPassword = "/*密码*/"
                })
                .SetParameter("VerifyCode","/*验证码*/")
                .SetHeader("Client-ID","0")
                .GetResponse()
                .ToText();
            var home = HttpClient.Create("http://www.demo.com", "GET")
                .GetResponse()
                .ToText();
            //
            var title = JQuery.Create(home).Find("head > title:first").Text();
             
         [示例4]:
		 	 /***********************************************************************************
			 * 该示例展示了常用方法综合使用情况;
			 *************************************************************************************/

             var result = HttpClient.Create("http://www.demo.com/User/Save", "POST")
				.SetParameter("Form1","111111")
                .SetParameter(new
                {
                    ID = 1,
                    Name = "Jom"
                })
				.SetHeader("Accept", "*/*; q=0.01")"
				.SetHeader(@"Accept:text/plain, */*; q=0.01
Accept-Encoding:gzip, deflate, sdch
Accept-Language:zh-CN,zh;q=0.8
Cache-Control:max-age=0
Connection:keep-alive
Cookie:A=GA1.2.790467438.1456117555
Host:www.cnblogs.com
If-Modified-Since:Fri, 27 May 2016 05:52:34 GMT
Referer:http://www.demo.com/
User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.154 Safari/537.36 LBBROWSER
X-Requested-With:XMLHttpRequest")
				.SetCookie("Name","Value","/","http://www.demo.com")
				.SetCookie(@"BIDUPSID=682141D2CB37AC99DFE449DD5A64C893; PSTM=1433326542;","/","http://www.demo.com")
                .SetFile("Photo","C:/1.jpg")
                .GetResponse()
                .ToText();

         [示例5]:
			 /***********************************************************************************
			 * 该示例展示获取响应的Cookie的方法，注意是否使用了全局共享cookie;
			 *************************************************************************************/

			HttpClient.LoadIECookie("www.demo.com");					//加载IE 浏览器中的Cookie
				
			var http=HttpClient.Create("http://www.demo.com", "GET");
			var httpResponse=http.GetResponse();
			var cookie=httpResponse.Cookies;							//当前响应的Cookie
			var cookie=HttpClient.FindCookie(http);						//私有的Cookie
			var cookie=HttpClient.FindCookie("www.demo.com");		//获取全局共享的Cookie
             
         [示例6]:
			 /***********************************************************************************
			 *    该示例展示了一些需要登录访问的情形（如API，站点采集等），设定全局拦截，自动免去
			 * 所有函数判断登录状态的烦恼；
			 * 	 除此之外，也适用于:
			 *		● 根据某种响应情况来重试访问；
			 *		● 记录请求日志；
			 *		● 修改请求参数(如批量添加请求头)；
			 *		● 其他你能想到的.....
			 *************************************************************************************/

            //全局拦截Request
            HttpClient.OnRequest((api) =>
            {
                Console.WriteLine("开始请求:"+api.Url.ToString()+"....");//记录日志
            });
            //全局拦截Response
            HttpClient.OnResponse((api) =>
            {
                //如果请求到了登录页面，尝试登录....
                if (api.TryCount < 20 && api.Response.HaveResponse && api.Response.ResponseUri.ToString().StartsWith("http://www.demo.com/Auth/Login"))
                {
                    var html = HttpClient.Create("http://www.demo.com/Auth/Login", "POST")
                     .SetParameter(new
                     {
                         LoginAccount = "sunliang@tidebuy.net",
                         LoginPassword = "1234"
                     })
                     .GetResponse();
                    api.TryAgain = true;//重新尝试之前的请求
                }
                else if (false)
                {//其他判断条件
                    api.TryAgain = true;//重新尝试之前的请求
                }
				 Console.WriteLine("请求结束:"+api.Url.ToString()+" "+api.Response.StatusCode);//记录日志
            });
            var homeDom = HttpClient.Create("http://www.demo.com", "GET")
                .GetResponse()
                .ToDom();
            //
            var title = homeDom.Find("title:first").Text();



         [示例7]:
			 /***********************************************************************************
			 * 该示例展示了替换系统默认的Json/Xml序列化/反序列化的方法;
			 *************************************************************************************/

			使用Newtonsoft.Json：
				HttpClient.JsonDeserialize = Newtonsoft.Json.JsonConvert.DeserializeObject;
				HttpClient.JsonSerialize = Newtonsoft.Json.JsonConvert.SerializeObject;
			




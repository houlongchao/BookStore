using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 添加测试数据
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Random r = new Random();
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBook(13);
            MessageBox.Show("添加成功！","提示");
        }

        private void AddBook(int num)
        {
            #region sql语句
            string sql = @"INSERT INTO [dbo].[Book]
           ([name]
           ,[author]
           ,[description]
           ,[categoryId]
           ,[imageUrl]
           ,[price]
           ,[pageview]
           ,[sale]
           ,[stock])
     VALUES
           (@name
           ,@author
           ,@description
           ,@cateforyId
           ,@imageUrl
           ,@price
           ,@pageview
           ,@sale
           ,@stock)";
            #endregion

            #region 插入num组数据
            for (int i = 0; i < num; i++)
            {
                SqlParameter[] pms = new SqlParameter[] {
                new SqlParameter("@name",GetName(i)),
                new SqlParameter("@author",GetAuthor(i)),
                new SqlParameter("@description",GetDescription(i)),
                new SqlParameter("@cateforyId",GetCateforyId(i)),
                new SqlParameter("@imageUrl",GetImageUrl(i)),
                new SqlParameter("@price",GetPrice(i)),
                new SqlParameter("@pageview",GetPageview(i)),
                new SqlParameter("@sale",GetSale(i)),
                new SqlParameter("@stock",GetStock(i)),

                };
                SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
            } 
            #endregion

        }
        #region Get***

        private string GetName(int i)
        {
            return "盗墓笔记"+i;
        }
        private string GetAuthor(int i)
        {
            return "南派"+r.Next(100)+"叔";
        }
        private string GetDescription(int i)
        {
            #region 数目介绍数组
            string[] strs =
                 {
                @"
新年的欢乐，春天的祝福都在《幼儿画报2015年第一季度合订本》中！围绕孩子生活告诉孩子安全自护的知识，教会孩子生活的智慧，让孩子插上想象的翅膀，自由地飞翔。更隆重推出了全新改版栏目——红袋鼠美食屋，耳目一新的故事和美食，一定带给孩子不一样的体验！孩子会在这样的故事中健康、开心、快乐地成长。2015年我们新增设了一个新的栏目，红袋鼠绘本故事。由中外作家、画家联合创作国际绘本系列，其中活动的文字、丰富的内容，精彩的画面，让阅读充满乐趣。",
                "《3-6岁综合能力开发趣味贴纸书：专注力+判断力+表现力+创造力+语言能力+理解力（套装共6册）》编辑推荐：专注力、判断力、表现力、创造力、语言能力、理解力，六大能力综合培养，每本书将相应能力分解为10个方面，通过大量构思巧妙、新鲜有趣的贴纸游戏，立体培养宝宝的多元思维能力。700张漂亮精美的贴纸让宝宝自由粘贴，动手又动脑，趣味无穷。每幅画面都有一个特定的场景，给宝宝充分的自由和思考空间。爸爸妈妈一开始不必要求宝宝一定要把贴纸贴得恰到好处，重点在于培养他们自由、自主思考的热情和能力。内文由中央美术学院优秀插画师精心绘制，场景丰富、色彩融合清新。贴纸贴完后，可以给宝宝当作很好的绘画样本。养成好习惯、培养好性格、锻炼主认知、语言表达、主动思考及动手能力，潜移默化中全面开发宝宝潜能。",
                "《3-6岁综合能力开发趣味贴纸书：专注力+判断力+表现力+创造力+语言能力+理解力（套装共6册）》编辑推荐：专注力、判断力、表现力、创造力、语言能力、理解力，六大能力综合培养，每本书将相应能力分解为10个方面，通过大量构思巧妙、新鲜有趣的贴纸游戏，立体培养宝宝的多元思维能力。700张漂亮精美的贴纸让宝宝自由粘贴，动手又动脑，趣味无穷。每幅画面都有一个特定的场景，给宝宝充分的自由和思考空间。爸爸妈妈一开始不必要求宝宝一定要把贴纸贴得恰到好处，重点在于培养他们自由、自主思考的热情和能力。内文由中央美术学院优秀插画师精心绘制，场景丰富、色彩融合清新。贴纸贴完后，可以给宝宝当作很好的绘画样本。养成好习惯、培养好性格、锻炼主认知、语言表达、主动思考及动手能力，潜移默化中全面开发宝宝潜能。",
                @"尽管任何一段历史都有它不可替代的独特性，可是，1978年－2008年的中国，却是最不可能重复的。在一个拥有13亿人口的大国里，僵化的计划经济体制日渐瓦解了，一群小人物把中国变成了一个巨大的试验场，它在众目睽睽之下，以不可逆转的姿态向商业社会转轨。
本书作者没有用传统的教科书或历史书的方式来写作这部著作，而是站在民间的角度，以真切而激扬的写作手法描绘了中国企业在改革开放年代走向市场、走向世界的成长、发展之路。改革开放初期汹涌的商品大潮；国营企业、民营企业、外资企业，这三种力量此消彼长、互相博弈的曲折发展；整个社会的躁动和不安……整部书稿中都体现得极为真切和实在。作者用激扬的文字再现出人们在历史创造中的激情、喜悦、呐喊、苦恼和悲愤。
作者不是将一些事件、人物孤立地展现在读者面前，他笔下的历史是可以触摸的，是可以被感知的，它充满了血肉、运动和偶然性。他把人物和事件放在一个国际和国内的政策、社会和当时的现实这样的大背景中，以整体和个别相结合的描述手法，将一部中国企业的曲折发展历程清晰地呈现在读者面前。
过去的三十年是如此的辉煌，特别对于沉默了百年的中华民族，它承载了太多人的光荣与梦想，它是几乎一代人共同成长的全部记忆。
targetReader:财经读者、历史爱好者、政府机关人员、大学生等",
               @"《历代经济变革得失》是作者近年来研究中国经济变革史的集大成之作，对中国历史上十数次重大经济变革的种种措施和实践作了系统的概述和比照，指明因革演变，坦陈利害得失，既高屋建瓴地总括了中国式改革的历史脉络，又剖析了隐藏在历代经济变革中的内在逻辑与规律。辩驳得失，以史为鉴，实不失为一部简明的“中国经济史”。
两千七百年前，春秋时期的管仲改制变法，使得齐国一跃成为霸主，傲视群雄；公元1069年，王安石在宋神宗的支持下推行新法，一时国库充实，北宋积贫积弱的局面为之缓解；公元1978年，邓小平开始实施改革开放政策，百年积弱的中国经济再度崛起，重回强国之列。在两千多年的时间里，中国经历了十数次重大的经济变革，每一次变革，都顺应社会发展而发生，也都对历史进程产生了重大影响。而今，新的社会发展又提出了继续变革的要求。",
                @"《大数据时代》是国外大数据系统研究的先河之作，本书作者维克托·迈尔·舍恩伯格被誉为“大数据商业应用第一人”，拥有在哈佛大学、牛津大学、耶鲁大学和新加坡国立大学等多个互联网研究重镇任教的经历，早在2010年就在《经济学人》上发布了长达14页对大数据应用的前瞻性研究。
维克托·尔耶·舍恩伯格在本书中前瞻性地指出，大数据带来的信息风暴正在变革我们的生活、工作和思维，大数据开启了一次重大的时代转型，并用三个部分讲述了大数据时代的思维变革、商业变革和管理变革。
维克托最具洞见之处在于，他明确指出，大数据时代最大的转变就是，放弃对因果关系的渴求，而取而代之关注相关关系。也就是说只要知道“是什么”，而不需要知道“为什么”。这颠覆了千百年来人类的思维惯例，对人类的认知和与世界交流的方式提出了全新的挑战。
　　《大数据时代》认为大数据的核心就是预测。大数据将为人类的生活创造前所未有的可量化的维度。大数据已经成为了新发明和新服务的源泉，而更多的改变正蓄势待发。书中展示了谷歌、微软、亚马逊、IBM、苹果、facebook、twitter、VISA等大数据先锋们最具价值的应用案例。",
                @"在《第三次工业革命：新经济模式如何改变世界》中，作者为我们描绘了一个宏伟的蓝图：数亿计的人们将在自己家里、办公室里、工厂里生产出自己的绿色能源，并在“能源互联网”上与大家分享，这就好像现在我们在网上发布、分享消息一样。能源民主化将从根本上重塑人际关系，它将影响我们如何做生意，如何管理社会，如何教育子女和如何生活。 
 我们正处于第二次工业革命和石油世纪的最后阶段。这是一个令人难以接受的严峻现实，因为这一现实将迫使人类迅速过渡到一个全新的能源体制和工业模式。否则，人类文明就有消失的危险。 
 作者敏锐地发现，历史上数次重大的经济革命都是在新的通讯技术和新的能源系统结合之际发生的。新的通讯技术和新的能源系统结合将再次出现——互联网技术和可再生能源将结合起来，将为第三次工业革命创造强大的新基础设施。 
 第一次工业革命使19世纪的世界发生了翻天覆地的变化；第二次工业革命为20世纪的人们开创了新世界；第三次工业革命同样也将在21世纪从根本上改变人们的生活和工作。 
 我们即将步入一个“后碳”时代。人类能否可持续发展，能否避免灾难性的气候变化，第三次工业革命将是未来的希望。 
 未来，每一处建筑转都会变成能就地收集可再生能源的迷你能量采集器； 
 未来，将每一大洲的建筑转化为微型发电厂，以便就地收集可再生能源； 
 将氢和其他可储存能源储存在建筑里，利用社会全部的基础设施来储藏间歇性可再生能源； 
 未来，利用互联网技术将全球的电力网转化为能源共享网络，工作原理就像互联网一样； 
 未来，汽车、公交车、卡车、火车等构成的全球运输模式，使之成为由插电式和燃料电池型以可再生能源为动力的运输工具构成的交通运输网。 
 未来25年内，数百万的建筑——家庭住房、办公场所、大型商场、工业技术园区——将会既可作为发电厂，也可以作为住所。 
 未来，家庭居民可以在自己的房顶上安装太阳能电池板，这些电池板能生产出足够的电力，满足房子所需的电能。如果有剩余，则可以出售给发电厂。 
 你准备好了吗？你的公司准备好了吗？中国准备好了吗？ ",
                @"你还在为铺天盖地的财经信息而困扰吗？它们夹杂着大量术语以至于难以理解，或者看起来与自己无关而不必关注。你可能需要一本帮助你看懂这些信息的通俗读物。《用地图看懂世界经济》就是这样一本内容丰富、轻松易读、兴味盎然的小书。

　　它采用短文加地图的形式，通过近一百张地图，将通货膨胀、股价涨跌、黄金保值、能源价格、贸易组织、福利政策等70个重要经济问题清晰易懂地呈现于纸上，读者一看便能了然于胸，对掌握经济关键的人流、物流、资金流了如指掌。它也强调帮助人们培养自己的经济观点，可作为每个人跟上世界的入门书和提高自身竞争力的自救工具书。

　　《用地图看懂世界经济》第一版出版后，迅速畅销日本，繁体版更是一年内28次印刷，成为最畅销的经济通俗读物。本书是其最新版本。
",
                @"全球最大的便利店连锁公司创始人——铃木敏文，结合40多年零售经验，《零售的哲学：7-Eleven便利店创始人自述》为你讲述击中消费心理的零售哲学。铃木敏文的很多创新，现在已经成为商界常识，本书把那些不可思议的零售创新娓娓道来。关于零售的一切：选址、订货、销售、物流、管理……他一次又一次地在一片反对声中创造出零售界的新纪录。
　　翻开《零售的哲学：7-Eleven便利店创始人自述》，看铃木敏文如何领导7-11冲破层层阻碍，成为世界第一的零售哲学"
            };
            #endregion
            return strs[r.Next(strs.Count())];
        }
        private int GetCateforyId(int i)
        {
            return i%5+1;
        }
        private string GetImageUrl(int i)
        {
            return "Assets/Images/Books/10"+(i%15+1).ToString("00")+".jpg";
        }
        private double GetPrice(int i)
        {
            return r.NextDouble()*100+100;
        }
        private int GetPageview(int i)
        {
            return i % 20 + 30;
        }
        private int GetSale(int i)
        {
            return i%20+30;
        }
        private int GetStock(int i)
        {
            return i % 20 + 30;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser();
            MessageBox.Show("添加成功！", "提示");
        }

        private void AddUser()
        {
            string sql = @"INSERT INTO [dbo].[Customer]
           ([username]
           ,[password]
           ,[sex]
           ,[telephone]
           ,[description]
           ,[address]
           ,[email]
           ,[actived]
           ,[code]
           ,[role])
     VALUES
           (@username
           ,@password
           ,@sex
           ,@telephone
           ,@description
           ,@address
           ,@email
           ,@actived
           ,@code
           ,@role)";

            //admin     21232f297a57a5a743894a0e4a801fc3
            //user      ee11cbb19052e40b07aac0ca060c23ee
            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@username","admin"),
                new SqlParameter("@password","21232f297a57a5a743894a0e4a801fc3"),
                new SqlParameter("@sex","1"),
                new SqlParameter("@telephone","110"),
                new SqlParameter("@description","管理员"),
                new SqlParameter("@address","管理员地址"),
                new SqlParameter("@email","admin@bookstore.com"),
                new SqlParameter("@actived","0"),
                new SqlParameter("@code","0"),
                new SqlParameter("@role","1")

            };
            SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);

            pms = new SqlParameter[]
            {
                new SqlParameter("@username","user"),
                new SqlParameter("@password","ee11cbb19052e40b07aac0ca060c23ee"),
                new SqlParameter("@sex","0"),
                new SqlParameter("@telephone","120"),
                new SqlParameter("@description","用户"),
                new SqlParameter("@address","用户地址"),
                new SqlParameter("@email","user@bookstore.com"),
                new SqlParameter("@actived","0"),
                new SqlParameter("@code","0"),
                new SqlParameter("@role","0")

            };
            SqlHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
    }
}

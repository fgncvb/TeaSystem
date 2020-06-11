
using Aspose.Words;

namespace Common_Module.FileTool
{
    public class WordFileHelper
    {
        public void CreateTestPaper(string content,string savepath)
        {
            string doctemplatepath = DocTemplatePath(); // 获取Word模板文件路径

            Document doc = new Document();
            DocumentBuilder docbuilder = new DocumentBuilder(doc);

            Section section = docbuilder.CurrentSection;
            section.PageSetup.LeftMargin = 36.4; // 内容与页面左边距
            section.PageSetup.TopMargin = 5; // 内容与页面上边距
            section.PageSetup.RightMargin = 25.7; // 内容与页面右边距
            section.PageSetup.BottomMargin = 30; // 内容与页面下边距

            section.PageSetup.PageWidth = 750;
            section.PageSetup.PageHeight = 5000;

            Aspose.Words.Font font = docbuilder.Font;
            font.Size = 13;
            //font.Bold = false;
            //font.Color = Color.Red;
            font.Name = "Arial";


            //docbuilder.Write("雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁雄关漫道真如铁");

            //docbuilder.InsertBreak(BreakType.LineBreak); // 当前页中插入空行
            //docbuilder.InsertBreak(BreakType.LineBreak); // 当前页中插入空行

            //docbuilder.InsertHtml("<table border='0' cellpadding='0' cellspacing='0' style='width:100%;text-align:center;'><tr><td style='height:35px;border-top:solid 1px #000;'>1</td><td>2</td><td>3</td><td>4</td><td>5</td><td>6</td></tr></table>");
            //docbuilder.InsertHtml("<img alt='' width='320' height='240' src='https://p3.ssl.cdn.btime.com/t010608c9db47b81e51.jpg?size=640x426' />");
            //docbuilder.InsertHtml("<img alt='' width='320' height='240' src='https://p0.ssl.cdn.btime.com/t015464ec52443b63a6.jpg?size=600x720' />");
            //docbuilder.InsertBreak(BreakType.LineBreak);
            //docbuilder.InsertHtml("<img alt='' width='320' height='240' src='https://p3.ssl.cdn.btime.com/t010608c9db47b81e51.jpg?size=640x426' />");
            //docbuilder.InsertHtml("<img alt='' width='320' height='240' src='https://p0.ssl.cdn.btime.com/t015464ec52443b63a6.jpg?size=600x720' />");
            //docbuilder.InsertHtml("<div style='width:100px;height:120px;background-color:silver;font-weight:bold;color:orangered;'>而今迈步从头越</div>");

            docbuilder.InsertHtml(content);
            doc.Save(savepath);
        }

        private string DocTemplatePath()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + "Template\\doctemplate.docx";
        }
    }
}

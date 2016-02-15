using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SkypeDashboard {
    public class SkypeDbContext : DbContext {
        public SkypeDbContext(DbConnection connection):base (connection,true) {
        }
        public DbSet<ConvertedMessage> Messages { get; set; }
    }
    public class ConvertedMessage {
        static string ProcessXml(string bodyXml) {
            if(bodyXml == null)
                return string.Empty;
            int index = 0;
            StringBuilder sb = new StringBuilder();
            while(index < bodyXml.Length) {
                int openBraceIndex = bodyXml.IndexOf('<', index);
                if(openBraceIndex >= 0) {
                    sb.Append(bodyXml.Substring(index, openBraceIndex - index));
                    int spaceIndex = bodyXml.IndexOf(' ', openBraceIndex);
                    if(spaceIndex > 0) {
                        string tag = string.Format("</{0}>", bodyXml.Substring(openBraceIndex + 1, spaceIndex - openBraceIndex - 1));
                        int closingTagIndex = bodyXml.IndexOf(tag, spaceIndex);
                        if(closingTagIndex > 0) {
                            index = closingTagIndex + tag.Length;
                            continue;
                        }
                    }
                }
                sb.Append(bodyXml.Substring(index));
                break;
            }
            return sb.ToString();
        }
        string body;

        public static string ConvertAuthor(string author) {
            switch(author) {
            case "Мария Козлова":
                return "Alexey Kozlov";
            case "Corporation (IlyaScherbakov)":
                return "Ilya Scherbakov";
            default:
                return author;
            }
        }
        public long id { get; set; }
        public string Author { get; set; }
        public string Body {
            get {
                if(body != null)
                    return body;
                body = ProcessXml(BodyXml);
                return body;
            }
        }


        public DateTime DateTime { get; set; }
        public string ChatName { get; set; }
        public string BodyXml { get; set; }
        public int TextLength { get { return Body.Length; } }

    }
}

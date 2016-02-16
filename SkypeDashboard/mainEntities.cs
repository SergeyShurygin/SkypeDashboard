using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SkypeDashboard {
    public partial class mainEntities : DbContext {
        //static IList<long> chatIds;
        //IList<long> ChatIds {
        //    get {
        //        if(chatIds == null) {
        //            chatIds = ChatsOnly.Select(c => c.id).ToList();
        //        }
        //        return chatIds;
        //    }
        //}
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp) {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        
        public IEnumerable<ConvertedMessage> MessageList {
            get {
                foreach(var cm in (
                           from m in this.Messages
                           join c in this.Conversations on m.convo_id equals c.id

                           select new { m.id, c.displayname, m.from_dispname, m.body_xml, m.timestamp, ChatID = c.id })) {
                    IList<long> ids = ViewerForm.ChatIds != null ? ViewerForm.ChatIds : ChatsOnly.Select(c => c.id).ToList();
                    if(ids.Any(id => id == cm.ChatID))
                        yield return new ConvertedMessage() { id = cm.id, ChatName = cm.displayname, Author = ConvertedMessage.ConvertAuthor(cm.from_dispname), BodyXml = cm.body_xml, DateTime = UnixTimeStampToDateTime(Convert.ToDouble(cm.timestamp)) };
                }
            }
        }

        public IQueryable<Conversations> ChatsOnly {
            get {
                return from c in this.Conversations
                       where (from p in this.Participants
                              where p.convo_id == c.id
                              select p.id).Count() > 2
                       select c;
            }
        }

    }
}

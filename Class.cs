namespace SignalRTest.MessageDatas
{
    public class MessageData
    {
        public string SenderName { get; set; }
        public string Message { get; set; }
        public DateTime SendDateTime { get; set; }
        
        public MessageData(string senderName, string message)
        {
            SenderName = senderName;
            Message = message;
            SendDateTime = DateTime.Now;
        }
    }
}

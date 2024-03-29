
namespace PromoCodesManager.Domain.Entities
{
    public enum HistoryEventType
    {
        Created,
        Updated,
        Deleted,
        Used
    }

    public class HistoryEvent
    {
        public PromoCode PromoCode { get; set; }
        public HistoryEventType EventType { get; set; }
        public DateTime EventDate { get; set; }
        //User performing the action
    }
}

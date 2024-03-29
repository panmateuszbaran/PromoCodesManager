using PromoCodesManager.Domain.Entities;

namespace PromoCodesManager.Business
{
    public static class GlobalHistoryPlaceholder
    {
        public static List<HistoryEvent> History { get; set; } = new List<HistoryEvent>();
    }
}

namespace GratShiftSaveApi.Models;

    public class GratShiftResponse
    {
        public List<GratShift> GratShifts { get; set; } = new List<GratShift>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }    
        public int PageSize { get; set; }
    }
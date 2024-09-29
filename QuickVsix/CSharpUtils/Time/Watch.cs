using System;

namespace CSharpUtils
{
   public class Watch
   {
      public virtual DateTime DateTimeNow()
      {
         DateTime dateTimeNow = DateTime.Now;
         return dateTimeNow;
      }

      public virtual DateTime DateToday()
      {
         DateTime dateToday = DateTime.Today;
         return dateToday;
      }

      public virtual string DateTodayString()
      {
         DateTime dateToday = DateTime.Today.Date;
         string dateTodayString = dateToday.ToString("yyyy-MM-dd");
         return dateTodayString;
      }

      public virtual string DateTimeNowString()
      {
         string dateTimeNowString = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
         return dateTimeNowString;
      }
   }
}

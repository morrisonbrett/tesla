using System;

 namespace tesla
 {
     public class Thing
     {
         public int Get(int number) => Newtonsoft.Json.JsonConvert.DeserializeObject<int>($"{number}");
     }
 }
 
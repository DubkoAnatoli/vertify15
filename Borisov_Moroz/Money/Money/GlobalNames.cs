using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Money
{
    class GlobalNames
    {
        public static List<User> Users = new List<User>();
        public static string name;                                              //имя пользователя
        public static List<List<String>> plus = new List<List<String>>();       //доходы
        public static List<List<String>> minus = new List<List<String>>();      //расходы
        public static List<List<String>> dream = new List<List<String>>();      //мечта
        public static List<List<String>> one = new List<List<String>>();        //единовременные затраты
        public static bool rezalt;
        public static bool admin;                                               //администратор
        public static int nomerUsera;                                           // номер залогиненного пользователя в Users
        public static bool clearR;                                              // очищать расходы
        public static bool clearD;                                              // очищать доходы
        public static bool clearDream;                                          // очищать мечту
        public static bool clearOne;                                            // очищать разовые траты
        public static Font f;                                                   //шрифт
        public static bool [] rashodi = new bool [5];
        public static bool [] dohodi = new bool [5];
        public static double kurs;                                                 //курс валюты
    }
}

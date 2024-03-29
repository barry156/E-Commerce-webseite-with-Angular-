﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IShopReader
{
    // List<Article> GetArticlesByOrderID(int orderID);
    string GetArticlesByOrderID(int orderID);

    string ReadArticleByID(int articleID);

    string ReadAllCustomer();
    string ReadAllArticles();
    string ReadAllOrders();

    //Wichtig
    string Login(string email);
    string getOrder(int customerId);
}
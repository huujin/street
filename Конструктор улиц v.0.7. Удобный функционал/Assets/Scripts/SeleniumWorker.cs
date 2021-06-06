using OpenQA.Selenium;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeleniumWorker:MonoBehaviour
{
    IWebDriver driver;

    public void OpenOSM()
    {
        driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
        driver.Navigate().GoToUrl("http://openstreetmap.ru/");
        driver.Manage().Window.Maximize();
    }
}

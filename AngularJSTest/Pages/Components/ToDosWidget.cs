﻿
using OpenQA.Selenium.Interactions;

namespace AngularJSTest.Pages.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    using AngularJSTest.Models;
    using AngularJSTest.Resources;
    using AngularJSTest.Service;

    using NUnit.Framework;

    using OpenQA.Selenium;

    /// <summary>
    /// The to dos widget.
    /// </summary>
    public class ToDosWidget : ComponentTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToDosWidget"/> class.
        /// </summary>
        /// <param name="driver">
        /// The driver.
        /// </param>
        public ToDosWidget(IWebDriver driver)
            : base(driver)
        {          
        }

        /// <summary>
        /// The click filter.
        /// </summary>
        /// <param name="filter">
        /// The filter.
        /// </param>
        public void ClickFilter(Enums.Filters filter)
        {
            string enumLctMask = "//ul[@id='filters']/li/a[.='{0}']";
            Driver.FindElement(By.XPath(string.Format(enumLctMask, filter))).Click();
        }

        /// <summary>
        /// The get number of items.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetNumberOfItems()
        {
           return int.Parse(Driver.FindElement(By.XPath("//span[@id='todo-count']/strong")).Text);
        }

        /// <summary>
        /// Get to dos list.
        /// </summary>
        /// <returns>
        /// ToDos list <see cref="List"/>.
        /// </returns>
        public List<ToDo> GetToDos()
        {
            Driver.WaitForPageReady();
            Driver.DisableTimeout();
            var toDoElements = Driver.FindElements(By.XPath("//*[@id='todo-list']//li"));
            Driver.SetTimeout();

            List<ToDo> toDos = new List<ToDo>();
            if (toDoElements.Count > 0)
            {
                foreach (var el in toDoElements)
                {
                    ToDo toDo = new ToDo(el);
                    toDo.Name = el.FindElement(By.XPath(".//label")).Text;
                    toDo.Completed = el.GetAttribute("class").Contains("completed");
                    toDos.Add(toDo);
                }
            }

            return toDos;
        }

        /// <summary>
        /// The get not completed to dos.
        /// </summary>
        /// <returns>
        /// The list of active to dos <see cref="List"/>.
        /// </returns>
        public List<ToDo> GetActiveToDos()
        {
            return this.GetToDos().Where(e => !e.Completed).ToList();
        }

        /// <summary>
        /// The get completed to dos.
        /// </summary>
        /// <returns>
        /// The list of active to dos <see cref="List"/>.
        /// </returns>
        public List<ToDo> GetCompletedToDos()
        {
            return this.GetToDos().Where(e => e.Completed).ToList();
        }


        /// <summary>
        /// The click clear completed button.
        /// </summary>
        public void ClickClearCompletedButton()
        {
            Driver.WaitForPageReady();
            Driver.FindElement(By.Id("clear-completed")).Click();
        }

        /// <summary>
        /// The check to do item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        public void CheckToDoItem(string itemName)
        {
            this.ClickOnToDoItem(itemName, true);
        }

        /// <summary>
        /// The uncheck to do item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        public void UncheckToDoItem(string itemName)
        {
            this.ClickOnToDoItem(itemName, false);
        }

        /// <summary>
        /// The remove to do item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        public void RemoveToDoItem(string itemName)
        {
            var item = this.GetToDos().First(e => e.Name.Equals(itemName));
            var removeButton = item.ParentElement.FindElement(By.XPath(".//button[@class='destroy']"));
            this.Driver.HoverElement(item.ParentElement);
            removeButton.Click();
        }

        /// <summary>
        /// The select all to do items.
        /// </summary>
        public void CheckAllToDoItems()
        {
            Driver.FindElement(By.Id("toggle-all")).Click();
        }

        /// <summary>
        /// The add new to do item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        public void AddNewToDoItem(string itemName)
        {
            Driver.FindElement(By.Id("new-todo")).SendKeys(itemName + Keys.Enter);
        }

        public void EditItemsName(string oldName, string newName)
        {
            var toDoItem = this.GetToDos().First(e => e.Name.Equals(oldName));
            Driver.DoubleClick(toDoItem.ParentElement);
            var editField = toDoItem.ParentElement.FindElement(By.XPath(".//input[contains(@class, 'edit')]"));
            Actions action = new Actions(Driver);            
            action.KeyDown(Keys.Control).SendKeys("a").KeyUp(Keys.Control).SendKeys(Keys.Delete).Perform();
            editField.SendKeys(newName + Keys.Enter);           
        }

        #region private methods

        /// <summary>
        /// The click on to do item.
        /// </summary>
        /// <param name="itemName">
        /// The item name.
        /// </param>
        /// <param name="complete">
        /// complete.
        /// </param>
        private void ClickOnToDoItem(string itemName, bool complete)
        {
            var item = this.GetToDos().First(e => e.Name.Equals(itemName));

            if (item.Completed != complete)
            {
                item.ParentElement.FindElement(By.XPath(".//input[@type='checkbox']")).Click();
            }
        }

        #endregion
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Text.RegularExpressions;

namespace J
{
    public class JLogin : MonoBehaviour
    {
        /// <summary>
        /// Called when the system realizes that a login operation was succesfull OR that the game already holds valid seession information
        /// </summary>
        public UnityEvent OnLogged;

        /// <summary>
        /// Contains the user field data
        /// </summary>
        public InputField UserField;

        /// <summary>
        /// The password for the account
        /// </summary>
        public InputField PasswordField;

        /// <summary>
        /// Button that calls the login function, should be disabled until both input fields are setup
        /// </summary>
        public Button LoginButton;

        /// <summary>
        /// Contains the type of user that logs in.
        /// </summary>
        public Dropdown TypeDropdown;

        /// <summary>
        /// Shows the error when a login attempt fails
        /// </summary>
        public Text ErrorText;

        /// <summary>
        /// If when starting, the login method should look for a valid previous session and use that instead
        /// </summary>
        public bool UsePreviousSession = true;

        //Init
        private void Start()
        {
            LoginButton.enabled = false;
            ErrorText.enabled = false;

            if(UsePreviousSession && JRemoteSession.Instance.SessionData.IsValidSession())
            {
                OnLogged?.Invoke();
            }
        }

        //Called when the values of the text fields change, to validate and enable/disable the "Login" button
        public void OnInputFieldUpdated()
        {
            string RegexPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                       + "@"
                                       + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

            //Only enable Login button when both fields are valid
            if (Regex.IsMatch(UserField.text, RegexPattern) && PasswordField.text.Length > 0)
            {
                LoginButton.enabled = true;
            }
            else
            {
                LoginButton.enabled = false;
            }
        }

        //Calls the login method from the SessionManager
        public void PerformLogin()
        {
            //Prepare the UI
            ErrorText.enabled = false;
            JRemoteSession.SessionType Type = (JRemoteSession.SessionType) TypeDropdown.value;

            JRemoteSession.Instance.AsyncLogin(UserField.text, PasswordField.text, Type, true, OnLoggin, OnLoginFailed);
        }

        private void OnLoggin()
        {
            OnLogged?.Invoke();
        }

        private void OnLoginFailed(int Code, string Error)
        {
            ErrorText.enabled = true;
            ErrorText.text = "Could not log in...";
        }
    }

}

using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, SYMPTOMS, DATE, CONFIRM
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone){
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public String OnMessage(String sInMessage)
        {
            String sMessage = "Welcome to Flu Fighters! Please enter your First name and Mobile number? (example: John Doe,+18888888888)";
            switch (this.nCur)
            {
                case State.WELCOMING:
                    this.nCur = State.SYMPTOMS;
                    break;
                case State.SYMPTOMS:
                    this.oOrder.Size = sInMessage;
                    this.oOrder.Save();
                    sMessage = "What are the Symptoms for your visit, for multiple symptoms write each with coma in between? (Example: Fever,Coughing,Backaches)";
                    this.nCur = State.DATE;
                    break;
                case State.DATE:
                    string sProtein = sInMessage;
                    sMessage = "What date are you available to visit us? (Example: 8 July 2022)";
                    this.nCur = State.CONFIRM;
                    this.oOrder.Save();
                    break;
                case State.CONFIRM:
                    string sCOnfirm = sInMessage;
                    sMessage = "Thank for booking an appointment";
                    this.nCur = State.WELCOMING;
                    this.oOrder.Save();
                    break;
                    

            }
            System.Diagnostics.Debug.WriteLine(sMessage);
            return sMessage;
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace airlineReservation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[,] seats = new string[5, 3];
        string[] rowChar = { "A", "B", "C", "D", "E" }; //for seat's row name
        string[] waitList = new string[10];
        string chartSeats = "";
        string allWait = "";
        int waitMarkCnt = 0;
        bool emptyChk = false;
        bool available = false;
        bool anyAvailable = false;
        bool chkName = false;
        bool chkSeat = false;
        int column = 0;
        int row = 0;

        //method for getting a row index from Letter
        private int getRow()
        {
            string rowChar = "";

            rowChar = listBoxRow.Text;

            switch (rowChar)
            {
                case "A":
                    row = 0;
                    break;
                case "B":
                    row = 1;
                    break;
                case "C":
                    row = 2;
                    break;
                case "D":
                    row = 3;
                    break;
                case "E":
                    row = 4;
                    break;
            }
            return row; 
        }
        private void showAllWaitlist()
        {
            allWait = "";
            for (int i = 0; i <waitList.Length; i++)
            {
                allWait += waitList[i]+"\n";
            }
            richTextBoxWaitList.Text = allWait;
        }

        //method for testing
        private void fillAll()
        {
            for (int i = 0; i <= seats.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= seats.GetUpperBound(1); j++)
                {
                    seats[i, j] = "Grace";
                }
            }
        }

        //method for showing all seats
        private void showAllSeats()
        {
            chartSeats = "";
            for (int i = 0; i <= seats.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= seats.GetUpperBound(1); j++)
                {
                    chartSeats += rowChar[i] + "-" + j + ": " + seats[i, j] + "\n";
                }
            }
            richTextBoxAll.Text = chartSeats;
        }

        //mothod for checking a seat is available
        private bool ChosenSeatAvailable()
        {
            int rowIndex = 0;

            column = int.Parse(listBoxColumn.Text);

            rowIndex=getRow();


            if (String.IsNullOrEmpty(seats[rowIndex, column]))
            {
                return available = true;
            }
            else
            {
                return available = false;
            }
        }

        private bool seatAvailable()
        {
            anyAvailable = false;

            for (int i = 0; i <= seats.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= seats.GetUpperBound(1); j++)
                {
                    if (String.IsNullOrEmpty(seats[i, j]))
                    {
                        anyAvailable = true;
                    }
                }
            }
            return anyAvailable;
        }

        //method to add usename on waiting list
        private void addWaitList()
        {
            //if waiting list is full, it will show message
            if (waitMarkCnt == 10)
            {
                MessageWait msgWait = new MessageWait();
                msgWait.Show();
            }
            else
            {
                //if waiting list is not full, it will add use name on the list
                waitList[waitMarkCnt] = textName.Text;
                waitMarkCnt++;

                AddWaiting addwait = new AddWaiting();
                addwait.Show();
            }
        }

        private bool chkNameSeat()
        {
            chkName = true;

            string userName = "";
            string rowValue = "";
            string columnValue = "";

            userName = textName.Text;
            rowValue = listBoxRow.Text;
            columnValue = listBoxColumn.Text;

            if (rowValue == "" || columnValue=="" || userName=="")
            {
                messageName msgName = new messageName();
                msgName.Show();

                chkName = false;
            }
            return chkName;
        }
        private void addBook()
        {
            // if it is already chosen, show message
            if (!ChosenSeatAvailable())
            {
                AlreadyMsg alMsg = new AlreadyMsg();
                alMsg.Show();
            }
            else
            {
                int rowIndex = 0;

                column = int.Parse(listBoxColumn.Text);

                rowIndex = getRow();

                seats[rowIndex, column] = textName.Text;
                Book book = new Book();
                book.Show();
            }
        }

        private bool chkCancelSeat()
        {
            chkSeat = true;

            string rowValue = "";
            string columnValue = "";

            rowValue = listBoxRow.Text;
            columnValue = listBoxColumn.Text;

            if (rowValue == "" || columnValue == "")
            {
                SeatError seaErr = new SeatError();
                seaErr.Show();

                chkSeat = false;
            }else if(String.IsNullOrEmpty(seats[getRow(), int.Parse(columnValue)]))
            {
                CancelEmp ccEmpty = new CancelEmp();
                ccEmpty.Show();

                chkSeat = false;
            }
            else
            {
                chkSeat = true;
            }
            return chkSeat;
        }

        private void deleteName(int r, int c)
        {
          //find a name from waiting list
            if (waitList[0]!="")
            {
                seats[r, c] = waitList[0];

                for (int i=0; i< waitList.Length-1; i++)
                {
                    waitList[i] = waitList[i + 1];
                    waitList[i + 1]= "";
                }
            }
            else
            {
                //update a seat to empty
                seats[r, c] = "";
            }
        }

        private void buttonFillAll_Click(object sender, EventArgs e)
        {
            //to debug
            fillAll();
        }

        private void buttonShowAll_Click(object sender, EventArgs e)
        {
            showAllSeats();
        }

        private void buttonStatus_Click(object sender, EventArgs e)
        {
            bool availChk = false;
            availChk = ChosenSeatAvailable();

            if (availChk)
            {
                textStatus.Text = "Available";
            }
            else
            {
                textStatus.Text = "Not Available";
            }
        }

        private void buttonShowWait_Click(object sender, EventArgs e)
        {
            showAllWaitlist();
        }

        private void buttonAddWait_Click(object sender, EventArgs e)
        {
            //if there is any available seat, it will show a message
            if (seatAvailable())
            {
                Message msg = new Message();
                msg.Show();
            } // if no available seat, it will add custmor name on waiting list
            else
            {
                addWaitList();
            }
        }

        private void buttonBook_Click(object sender, EventArgs e)
        {
            //check name, seat
            if (chkNameSeat())
            {
                //check a seat is already
                if (seatAvailable())
                {
                    addBook();
                } 
                //if a seat is already booked
                else
                {
                    addWaitList();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //first, check use chose a seat
            if (chkCancelSeat())
            {
                int rowIndex = 0;

                column = int.Parse(listBoxColumn.Text);

                //call a method to get a index of array
                rowIndex = getRow();

                deleteName(rowIndex, column);

                CacelMsg ccMsg = new CacelMsg();
                ccMsg.Show();
            }
        }
    }
}

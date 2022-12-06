using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WINFORMS___HW
{
    public partial class Form1 : Form
    {
        BooksDBEntities booksDB = new BooksDBEntities();
        Book book = new Book();
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            dataGridView1.DataSource = booksDB.Books.ToList();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Load Error !!\n--------------\n{ex}");
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentCell.RowIndex != -1)
            {
                int booksId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells["SerialNumber"].Value);

                book = booksDB.Books.Where(b => b.SerialNumber == booksId).FirstOrDefault();

                if(book != null)
                {
                    txtName.Text = book.BookName;
                    txtDescription.Text = book.BookDescription;
                    txtSerial.Text = book.SerialNumber.ToString();
                }
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (book != null )
                {
                    book.BookName = txtName.Text;
                    book.BookDescription = txtDescription.Text;


                    booksDB.Books.Add(book);
                    if (booksDB.SaveChanges() > 0)
                    {
                        MessageBox.Show($"Book Added Successfuly");
                        LoadData();
                    }
                }
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Book Add Error !!\n--------------\n{ex}");
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (book != null)
                {
                    book.BookName = txtName.Text;
                    book.BookDescription = txtDescription.Text;

                    booksDB.Entry(book).State = EntityState.Modified;

                    booksDB.SaveChanges();
                    MessageBox.Show("Book Edited Successfully");
                    LoadData();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Book Edit Error !!\n--------------\n{ex}");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(book != null)
                {
                    booksDB.Books.Remove(book);
                    booksDB.SaveChanges();
                    MessageBox.Show("Book Deleted Successfully");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Book Delete Error !!\n--------------\n{ex}");
            }
        }
    }
}

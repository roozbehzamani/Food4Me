using MainEnterpriseProject.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainEnterpriseProject.Models.Repository
{
    public class rep_mail
    {
        DataBase db=new DataBase();



        public bool Insert(Tab_mail t)

        {
            try 
	{	        
		         db.Tab_mail.Add(t);
                if(Convert.ToBoolean(db.SaveChanges()))
                {

                    return true;
                }else{


                    return false;
                }





                }
	
	catch 
	{
		return false;
		
	}
           }

        }


    }

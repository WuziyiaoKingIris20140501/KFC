using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelVp.CMS.Domain.Entity
{
    public class FAQEntity : BaseEntity
    {
        private List<FAQDBEntity> _faqdbentity;
        public List<FAQDBEntity> FAQDBEntity
        {
            get { return _faqdbentity; }
            set { _faqdbentity = value; }
        }
    }

    public class FAQDBEntity
    {  
        private int _ID;
	    public int ID
	    {
		    get { return _ID;}
		    set { _ID = value;}
	    }

        private string _QA_CODE;
	    public string QA_CODE
	    {
		    get { return _QA_CODE;}
		    set { _QA_CODE = value;}
	    }

        private string _QUSETION_USER;
	    public string QUSETION_USER
	    {
		    get { return _QUSETION_USER;}
		    set { _QUSETION_USER = value;}
	    }
	
        private string _ANSWER_USER;
	    public string ANSWER_USER
	    {
		    get { return _ANSWER_USER;}
		    set { _ANSWER_USER = value;}
	    }

        private string _QUSETION_HEAD;
	    public string QUSETION_HEAD
	    {
		    get { return _QUSETION_HEAD;}
		    set { _QUSETION_HEAD = value;}
	    }

        private string _ANSWER_BODY;
	    public string ANSWER_BODY
	    {
		    get { return _ANSWER_BODY;}
		    set { _ANSWER_BODY = value;}
	    }
    
        private string _CREATE_TIME;
	    public string CREATE_TIME
	    {
		    get { return _CREATE_TIME;}
		    set { _CREATE_TIME = value;}
	    }

        private string _UPDATE_TIME;
	    public string UPDATE_TIME
	    {
		    get { return _UPDATE_TIME;}
		    set { _UPDATE_TIME = value;}
	    }

        private int _SEQ;
        public int SEQ
        {
            get { return _SEQ; }
            set { _SEQ = value; }
        }
    }
}

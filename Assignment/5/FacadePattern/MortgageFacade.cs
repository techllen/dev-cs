﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern
{
    internal class MortgageFacade
    {
        Bank bk = new Bank();
        Loan ln = new Loan();
        Credit cr = new Credit();
        public bool MortgageApproval(MortgageApplicant mapp, ref string reason)
        {
            //getting the balances from the bank
            bool approved = false;
            double bal = bk.GetCurrentCheckingBalance(mapp.BankAccountNum);
            bal = bal + bk.GetSavingBalance(mapp.BankAccountNum);
            //checking the differences btn balance and outstanding amounts(from loan)
            bal = bal - ln.GetOutstandingLoans(mapp.LoanAccountNum);
            //deny for neg balances
            if (bal < 0)
            {
                approved = false;
                reason = "negative balances ";
            }
            //approval process
            //deny if asked amount is less than 20% of your balance, else approve if the creditrating is >= Good
            else
            {
                if (bal < 0.2 * mapp.LoanAmountAsked)
                {
                    approved = false;
                    reason = "Not enough balances";
                }
                else
                {
                    if (cr.CheckCredit(mapp.SSNum) < CreditRating.GOOD)
                    {
                        approved = false;
                        reason = "not good credit rating..";
                    }
                    else
                        approved = true;
                }
            }
            return approved;
        }
    }
}

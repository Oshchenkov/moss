﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC.Models;
using EC.Models.Database;
using EC.Common.Interfaces;
using EC.Core.Common;

namespace EC.Controllers
{
    public class NewController : Controller
    {
        ECEntities db = new ECEntities();
        GlobalFunctions glb = new GlobalFunctions();
        private IEmailAddressHelper m_EmailHelper = new EmailAddressHelper();

        // GET: New
        public ActionResult Index(string code, string email)
        {
            #region EC-CC Viewbag
            bool is_cc = glb.IsCC(Request.Url.AbsoluteUri.ToLower());
            ViewBag.is_cc = is_cc;
            string cc_ext = "";
            if (is_cc) cc_ext = "_cc";
            ViewBag.cc_extension = cc_ext;
            #endregion


            string _code = "";
            if (!string.IsNullOrEmpty(code))
                _code = code;

            ViewBag.code = _code;
            string _email = "";
            if (!string.IsNullOrEmpty(email))
                _email = email;

            ViewBag.email = _email;
            return View();
        }
        public ActionResult Company(string code)
        {
            string _code = "";
            if (!string.IsNullOrEmpty(code))
                _code = code;

            ViewBag.code = _code;
            #region EC-CC Viewbag
            bool is_cc = glb.IsCC(Request.Url.AbsoluteUri.ToLower());
            ViewBag.is_cc = is_cc;
            string cc_ext = "";
            if (is_cc) cc_ext = "_cc";
            ViewBag.cc_extension = cc_ext;
            #endregion
            return View();
        }

        public ActionResult Success(string show)
        {
            #region EC-CC Viewbag
            bool is_cc = glb.IsCC(Request.Url.AbsoluteUri.ToLower());
            ViewBag.is_cc = is_cc;
            string cc_ext = "";
            if (is_cc) cc_ext = "_cc";
            ViewBag.cc_extension = cc_ext;
            #endregion

            int _show = 0;
            if (!string.IsNullOrEmpty(show))
            {
                int.TryParse(show, out _show);

            }
            //int user_id = 2;
            user user = (user)Session[Constants.CurrentUserMarcker];
            if (user == null || user.id == 0)
                return RedirectToAction("Index", "Account");


            int user_id = user.id;
            UserModel um = new UserModel(user_id);
            ViewBag.user_nm = um._user.login_nm;
            ViewBag.password = um._user.password;
            CompanyModel cm = new CompanyModel(um._user.company_id);
            ViewBag.company_nm = cm._company.company_nm;

            if (_show == 1)
            {
                ViewBag.code = cm._company.company_code;
            }
            else
            {
                ViewBag.code = "";
            }
            ViewBag.logo = cm._company.path_en;

            return View();
        }
        public string CreateCompany(string code, string location, string company_name, string number, string first, string last, string email, string title, string description)
        {
            int company_id = 0;
            int user_id = 0;
            int location_id = 0;
            int language_id = 1;

            if ((string.IsNullOrEmpty(code)) || (string.IsNullOrEmpty(location)) || (string.IsNullOrEmpty(company_name)) || (string.IsNullOrEmpty(number)) || (string.IsNullOrEmpty(first)) || (string.IsNullOrEmpty(last)) || (string.IsNullOrEmpty(email)) || (string.IsNullOrEmpty(title)))
            {
                return App_LocalResources.GlobalRes.EmptyData;
            }
            if (glb.isCompanyInUse(company_name))
            {
                return App_LocalResources.GlobalRes.CompanyInUse;
            }
            if (!m_EmailHelper.IsValidEmail(email))
            {
                return App_LocalResources.GlobalRes.EmailInvalid;
            }
            #region CompanySaving
            int company_invitation_id = 0;
            int client_id = 0;
            if (!db.company_invitation.Any(t => ((t.is_active == 1) && (t.invitation_code.Trim().ToLower() == code.Trim().ToLower()))))
                return App_LocalResources.GlobalRes.InvalidCode;
            else
            {
                List<company_invitation> invitations = db.company_invitation.Where(t => ((t.is_active == 1) && (t.invitation_code.Trim().ToLower() == code.Trim().ToLower()))).ToList();
                company_invitation _invitation = invitations[0];
                company_invitation_id = _invitation.id;
                client_id = _invitation.created_by_company_id;

            }

            string company_code = glb.GenerateCompanyCode(company_name);



            company _company = new company();
            _company.company_nm = company_name.Trim();
            _company.company_invitation_id = company_invitation_id;
            _company.client_id = client_id;
            _company.status_id = 2;
            _company.registration_dt = DateTime.Now;
            _company.company_code = company_code.Trim();
            _company.employee_quantity = number;
            _company.language_id = language_id;

            _company.address_id = 1;
            _company.billing_info_id = 0;
            _company.contact_nm = first.Trim() + " " + last.Trim();
            _company.implicated_title_name_id = 1;
            _company.witness_show_id = 0;
            _company.show_location_id = 1;
            _company.show_department_id = 1;

            _company.notepad_en = "";
            _company.notepad_fr = "";
            _company.notepad_es = "";
            _company.notepad_ru = "";
            _company.notepad_ar = "";

            _company.path_en = "";
            _company.path_fr = "";
            _company.path_es = "";
            _company.path_ru = "";
            _company.path_ar = "";

            _company.alert_en = "";
            _company.alert_fr = "";
            _company.alert_es = "";
            _company.alert_ru = "";
            _company.alert_ar = "";

            _company.last_update_dt = DateTime.Now;

            _company.user_id = 1;
            _company.time_zone_id = 8;
            _company.step1_delay = 2;
            _company.step1_postpone = 2;
            _company.step2_delay = 2;
            _company.step2_postpone = 2;
            _company.step3_delay = 14;
            _company.step3_postpone = 2;
            _company.step4_delay = 5;
            _company.step4_postpone = 2;
            _company.step5_delay = 7;
            _company.step5_postpone = 2;
            _company.step6_delay = 7;
            _company.step6_postpone = 2;

            string url = Request.Url.AbsoluteUri.ToLower();
            string _url = "registration";
            if(url.Contains("cai."))
                _url = "cai";
            if (url.Contains("demo."))
                _url = "demo";
            if (url.Contains("stark."))
                _url = "stark";
            _company.subdomain = _url;
           ////// _company.reseller = client_id;

            try
            {
                db.company.Add(_company);
                db.SaveChanges();
                company_id = _company.id;
            }
            catch (Exception ex)
            {
                return App_LocalResources.GlobalRes.CompanySavingFailed;
            } 
            #endregion

            #region Location Saving
            if (company_id != 0)
            {
                // LocationSavingFailed
                company_location _location = new company_location();
                _location.location_en = location.Trim();
                _location.location_fr = location.Trim();
                _location.location_es = location.Trim();
                _location.location_ru = location.Trim();
                _location.location_ar = location.Trim();
                _location.status_id = 2;
                _location.company_id = company_id;
                _location.client_id = client_id;
                _location.last_update_dt = DateTime.Now;
                _location.user_id = 1;
                try
                {
                    db.company_location.Add(_location);
                    db.SaveChanges();
                    location_id = _location.id;
                }
                catch (Exception ex)
                {
                    return App_LocalResources.GlobalRes.LocationSavingFailed;
                } 
            }
            else
            {
                return App_LocalResources.GlobalRes.CompanySavingFailed;
            }

           #endregion

            #region Departments
            /// easy part - Administrative, Accounting, Management, Sales and Support 
            /// 
            List<string> list_departments = new List<string>();
            list_departments.Add(App_LocalResources.GlobalRes.Administrative);
            list_departments.Add(App_LocalResources.GlobalRes.Accounting);
            list_departments.Add(App_LocalResources.GlobalRes.Management);
            list_departments.Add(App_LocalResources.GlobalRes.Sales);
            list_departments.Add(App_LocalResources.GlobalRes.Support);


            if (company_id != 0)
            {
                foreach (string _dep in list_departments)
                {
                    if (_dep.Trim().Length > 0)
                    {
                        company_department _department = new company_department();
                        _department.department_en = _dep.Trim();
                        _department.department_ar = _dep.Trim();
                        _department.department_es = _dep.Trim();
                        _department.department_fr = _dep.Trim();
                        _department.department_ru = _dep.Trim();
                        //         _department.department_en = _dep.Trim();
                        _department.client_id = client_id;
                        _department.company_id = company_id;
                        _department.last_update_dt = DateTime.Now;
                        _department.status_id = 2;


                        try
                        {
                            db.company_department.Add(_department);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return App_LocalResources.GlobalRes.DepartmentSavingFailed;
                        }
                    }
                }
            }
            #endregion

            #region Incident types
            if (company_id != 0)
            {
                List<string> list_types = db.secondary_type_mandatory.Where(t => t.type_id == 1 && t.status_id == 2).Select(item => item.secondary_type_en).ToList();
               // list_types.Add(App_LocalResources.GlobalRes.Administrative);

                foreach (string _types in list_types)
                {
                    if (_types.Trim().Length > 0)
                    {
                        company_secondary_type _incident_type = new company_secondary_type();
                        _incident_type.secondary_type_en = _types.Trim();
                        _incident_type.secondary_type_ar = _types.Trim();
                        _incident_type.secondary_type_es = _types.Trim();
                        _incident_type.secondary_type_fr = _types.Trim();
                        _incident_type.secondary_type_ru = _types.Trim();
                        //       _incident_type.secondary_type_ar = _types.Trim();
                        _incident_type.client_id = client_id;
                        _incident_type.company_id = company_id;
                        _incident_type.last_update_dt = DateTime.Now;
                        _incident_type.status_id = 2;
                        _incident_type.type_id = 1;
                        _incident_type.weight = 200;
                        _incident_type.parent_secondary_type = 0;

                        try
                        {
                            db.company_secondary_type.Add(_incident_type);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return App_LocalResources.GlobalRes.IncidentTypeSavingFailed;
                        }
                    }
                }
            }
            #endregion

            #region Relationship
            if (company_id != 0)
            {
                List<string> list_relationship = db.relationship.Select(item => item.relationship_en).ToList();
                // list_types.Add(App_LocalResources.GlobalRes.Administrative);


                foreach (string _relationships in list_relationship)
                {
                    if ((_relationships.Trim().Length > 0) && (_relationships.Trim().ToLower() != "other"))
                    {
                        company_relationship _company_relationship = new company_relationship();
                        _company_relationship.relationship_en = _relationships.Trim();
                        _company_relationship.relationship_ar = _relationships.Trim();
                        _company_relationship.relationship_es = _relationships.Trim();
                        _company_relationship.relationship_fr = _relationships.Trim();
                        _company_relationship.relationship_jp = _relationships.Trim();
                        _company_relationship.relationship_ru = _relationships.Trim();
                        _company_relationship.client_id = client_id;
                        _company_relationship.company_id = company_id;
                        ///??     _company_relationship.last_update_dt = DateTime.Now;
                        _company_relationship.status_id = 2;

                        try
                        {
                            db.company_relationship.Add(_company_relationship);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return App_LocalResources.GlobalRes.RelationshipsSavingFailed;
                        }
                    }
                }
            }
            #endregion



            #region Anonymity
            if (company_id != 0)
            {
                List<int> list_anonymity = db.anonymity.Select(item => item.id).ToList();

                foreach (int _anon in list_anonymity)
                {
                    if (_anon > 0)
                    {
                        company_anonymity _anonymity = new company_anonymity();
                        _anonymity.company_id = company_id;
                        _anonymity.last_update_dt = DateTime.Now;
                        _anonymity.status_id = 2;
                        _anonymity.anonymity_id = _anon;
                        _anonymity.user_id = 1;

                        try
                        {
                            db.company_anonymity.Add(_anonymity);
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            return App_LocalResources.GlobalRes.AnonymitySavingFailed;
                        }
                    }
                }
            } 
            #endregion

            string login = glb.GenerateLoginName(first, last);
            string pass = glb.GeneretedPassword();

            #region User Saving
            if (company_id != 0)
            {
                user _user = new user();
                _user.first_nm = first.Trim();
                _user.last_nm = last.Trim();
                _user.company_id = company_id;
                _user.role_id = 5;
                _user.status_id = 2;
                _user.login_nm = login.Trim();
                _user.password = pass.Trim();
                _user.photo_path = "";
                _user.email = email.Trim();
                _user.phone = "";
                _user.preferred_contact_method_id = 1;
                _user.title_ds = title.Trim();
                _user.employee_no = "";
                _user.notepad_tx = description.Trim();
                _user.question_ds = "";
                _user.answer_ds = "";
                _user.previous_login_dt = DateTime.Now;
                _user.previous_login_dt = null;
                _user.last_update_dt = DateTime.Now;
                _user.user_id = 1;
                _user.preferred_email_language_id = language_id;
                _user.notification_messages_actions_flag = 1;
                _user.notification_new_reports_flag = 1;
                _user.notification_marketing_flag = 1;
                _user.notification_summary_period = 1;
                _user.company_location_id = location_id;
                _user.location_nm = "";
                _user.sign_in_code = null;

                try
                {
                    db.user.Add(_user);
                    db.SaveChanges();
                    user_id = _user.id;
                }
                catch (Exception ex)
                {
                    return App_LocalResources.GlobalRes.UserSavingFailed;
                } 
            }
            else
            {
                return App_LocalResources.GlobalRes.CompanySavingFailed;
            }

            if (login != null && login.Length > 0)
            {
                UserModel userModel = new UserModel();
                var user = userModel.Login(login, pass);
                EC.Controllers.utils.AuthHelper.SetCookies(user, HttpContext);
                Session[Constants.CurrentUserMarcker] = user;
                Session["userName"] = "";
                Session["userId"] = user.id;


                #region Email to Case Admin

                if ((user.email.Trim().Length > 0) && m_EmailHelper.IsValidEmail(user.email.Trim()))
                {
                    List<string> to = new List<string>();
                    List<string> cc = new List<string>();
                    List<string> bcc = new List<string>();

                    to.Add(user.email.Trim());
                    ///     bcc.Add("timur160@hotmail.com");

                    EC.Business.Actions.Email.EmailManagement em = new EC.Business.Actions.Email.EmailManagement();
                    EC.Business.Actions.Email.EmailBody eb = new EC.Business.Actions.Email.EmailBody(1, 1, Request.Url.AbsoluteUri.ToLower());
                    eb.NewCompany(user.first_nm, user.last_nm, login.Trim(), pass.Trim(), company_name.Trim(), company_code.Trim());
                    string body = eb.Body;
                    em.Send(to, cc, App_LocalResources.GlobalRes.Email_Title_NewCompany, body, true);
                }
                

                #endregion

                return App_LocalResources.GlobalRes._Completed.ToLower();
            }
            else
                return App_LocalResources.GlobalRes.UserSavingFailed;
            #endregion
       



          //  return user_id.ToString();

        }

        public string CreateUser(string code, string first, string last, string email, string title, string description)
        {
            int company_id = 0;
            int user_id = 0;
            int location_id = 0;
            int language_id = 1;

            if ((string.IsNullOrEmpty(code)) || (string.IsNullOrEmpty(first)) || (string.IsNullOrEmpty(last)) || (string.IsNullOrEmpty(email)) || (string.IsNullOrEmpty(title)))
            {
                return App_LocalResources.GlobalRes.EmptyData;
            }

            if (!m_EmailHelper.IsValidEmail(email))
            {
                return App_LocalResources.GlobalRes.EmailInvalid;
            }

            if (db.invitation.Any(t => ((t.email.ToLower().Trim() == email.ToLower().Trim()) && (t.used_flag == 1) && (t.code.Trim().ToLower() == code.Trim().ToLower()))))
                return App_LocalResources.GlobalRes.AlreadyRegistered;

            int invitation_id = 0;

            if (!db.invitation.Any(t => ((t.email.ToLower().Trim() == email.ToLower().Trim()) && (t.used_flag == 0) && (t.code.Trim().ToLower() == code.Trim().ToLower()))))
                return App_LocalResources.GlobalRes.InvitationCodeMismatch;
            else
            {
                List<invitation> invitations = db.invitation.Where(t => ((t.email.ToLower().Trim() == email.ToLower().Trim()) && (t.used_flag == 0) && (t.code.Trim().ToLower() == code.Trim().ToLower()))).ToList();
                invitation _invitation = invitations[0];
                invitation_id = _invitation.id;
                try
                {
                    UserModel um = new UserModel(_invitation.sent_by_user_id);
                    CompanyModel cm = new CompanyModel(um._user.company_id);
                    company_id = cm._company.id;

                    List<string> _company_user_emails = new List<string>();
                    _company_user_emails = (db.user.Where(t => ((t.company_id == company_id) && (t.role_id != Constant.level_informant))).Select(t => t.email.Trim().ToLower())).ToList();

                    List<int> _company_user_ids = new List<int>();
                    _company_user_ids = (db.user.Where(t => ((t.company_id == company_id) && (t.role_id != Constant.level_informant))).Select(t => t.id)).ToList();

                    if (db.user.Any(u => ((u.email.ToLower().Trim() == email.ToLower().Trim() && u.role_id != 8 && _company_user_ids.Contains(u.id)))))
                        return App_LocalResources.GlobalRes.AlreadyRegistered;
                }
                catch
                {
                    invitation_id = 0;
                    company_id = 0;
                }
            }


            if (invitation_id == 0)
            {
                return App_LocalResources.GlobalRes.InvitationCompanyMismatch;
            }
            string login = glb.GenerateLoginName(first, last);
            string pass = glb.GeneretedPassword();

            #region User Saving
            if (company_id != 0)
            {
                #region Lowest Company Location ID

                List<company_location> company_locations = db.company_location.Where(t => ((t.status_id == 2))).OrderBy( t=> t.id).ToList();
                if(company_locations.Count > 0)
                    location_id = company_locations[0].id;

                #endregion

                user _user = new user();
                _user.first_nm = first.Trim();
                _user.last_nm = last.Trim();
                _user.company_id = company_id;
                _user.role_id = 6;
                _user.status_id = 2;
                _user.login_nm = login.Trim();
                _user.password = pass.Trim();
                _user.photo_path = "";
                _user.email = email.Trim();
                _user.phone = "";
                _user.preferred_contact_method_id = 1;
                _user.title_ds = title.Trim();
                _user.employee_no = "";
                _user.notepad_tx = description.Trim();
                _user.question_ds = "";
                _user.answer_ds = "";
                _user.previous_login_dt = DateTime.Now;
                _user.previous_login_dt = null;
                _user.last_update_dt = DateTime.Now;
                _user.user_id = 1;
                _user.preferred_email_language_id = language_id;
                _user.notification_messages_actions_flag = 1;
                _user.notification_new_reports_flag = 1;
                _user.notification_marketing_flag = 1;
                _user.notification_summary_period = 1;
                _user.company_location_id = location_id;
                _user.location_nm = "";
                _user.sign_in_code = null;

                try
                {
                    db.user.Add(_user);
                    db.SaveChanges();
                    user_id = _user.id;
                }
                catch (Exception ex)
                {
                    return App_LocalResources.GlobalRes.UserSavingFailed;
                }
            }
            else
            {
                return App_LocalResources.GlobalRes.InvitationCompanyMismatch;
            }

            if (login != null && login.Length > 0)
            {
                UserModel userModel = new UserModel();
                var user = userModel.Login(login, pass);
                EC.Controllers.utils.AuthHelper.SetCookies(user, HttpContext);
                Session[Constants.CurrentUserMarcker] = user;
                Session["userName"] = "";
                Session["userId"] = user.id;

                #region Email to Case Admin

                if ((user.email.Trim().Length > 0) && m_EmailHelper.IsValidEmail(user.email.Trim()))
                {
                    List<string> to = new List<string>();
                    List<string> cc = new List<string>();
                    List<string> bcc = new List<string>();

                    to.Add(user.email.Trim());
                    ///     bcc.Add("timur160@hotmail.com");

                    CompanyModel cm = new CompanyModel(user.company_id);

                    EC.Business.Actions.Email.EmailManagement em = new EC.Business.Actions.Email.EmailManagement();
                    EC.Business.Actions.Email.EmailBody eb = new EC.Business.Actions.Email.EmailBody(1, 1, Request.Url.AbsoluteUri.ToLower());
                    eb.NewUser(user.first_nm, user.last_nm, login.Trim(), pass.Trim());
                    string body = eb.Body;

                    string email_title = App_LocalResources.GlobalRes.Email_Title_NewUser;
                    email_title = email_title.Replace("[CompanyName]", cm._company.company_nm);
                    em.Send(to, cc, email_title, body, true);

                    #region New Mediator Arrived - message to all Supervising mediators
                    foreach (user _user in cm.AllSupervisingMediators(cm._company.id, true))
                    {
                        if ((_user.email.Trim().Length > 0) && m_EmailHelper.IsValidEmail(_user.email.Trim()))
                        {
                            to = new List<string>();
                            cc = new List<string>();
                            bcc = new List<string>();

                            to.Add(_user.email.Trim());
                            ///     bcc.Add("timur160@hotmail.com");

                            eb.NewUserArrived(_user.first_nm, _user.last_nm, user.first_nm, user.last_nm);
                            body = eb.Body;
                            em.Send(to, cc, email_title, body, true);
                        }
                    } 
                    #endregion
                }
                #endregion

                return App_LocalResources.GlobalRes._Completed.ToLower();
            }
            else
                return App_LocalResources.GlobalRes.UserSavingFailed;
            #endregion
        }

        public JsonResult IsCompanyAvailable(string company_nm)
        {
            JsonResult result_company = new JsonResult();

            if (company_nm.Length > 0)
            {
                bool result = glb.isCompanyInUse(company_nm);
                //TODO: Do the validation

                if (!result)
                    result_company.Data = 0;
                else
                    result_company.Data = 1;
            }
            else
            {
                result_company.Data = 1;
            }
            return result_company;

        }
    }
}
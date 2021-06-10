using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IR_MVCwebsite.Models;
using Annytab.Stemmer;
using System.Data.Entity;

namespace IR_MVCwebsite.Controllers
{

    public class searchController : Controller
    {
        // GET: search

        HashSet<string> r = new HashSet<string>();
        HashSet<string> final_r = new HashSet<string>();
        int[] sub;
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult resultlinks(search input)
        {
            //r.links.Clear();
            string search_txt = input.seach_phrase;
            Stemmer stemmer = new Annytab.Stemmer.EnglishStemmer();
            testEntities1 ent = new testEntities1();
           


            //var query3 = from tab3 in ent.crawler_data select tab3;
            //List<before_stemmer> before_stemm = query1.ToList();
            //List<tockenz> tokenz = query2.ToList();
            //List<crawler_data> crawlerData = query3.ToList();


            if (search_txt.Equals("")) { return View(r); }
            else if (search_txt.StartsWith("\"") && search_txt.EndsWith("\""))
            {
                search_txt = search_txt.Remove(0, 1);
                search_txt = search_txt.Remove(search_txt.Length - 1, 1);
                string[] search_arr = search_txt.Split(' ');
                string[] arrayStemmer = stemmer.GetSteamWords(search_arr);
                List<List<tockenz>> tock = new List<List<tockenz>>();
                ent.Configuration.ProxyCreationEnabled = false;
                int phrasecount = 0;
                if (arrayStemmer.Length > 1)
                {
                    
                    /// List<List<before_stemmer>> before_s = new List<List<before_stemmer>>();
                    for (int i = 0; i < arrayStemmer.Length; i++)
                    {
                        if (i + 1 == arrayStemmer.Length && i != 0)
                            break;
                        string st1 = arrayStemmer[i];
                        string st2 = arrayStemmer[i + 1];
                        var query1 = from tab1 in ent.tockenzs where tab1.sring.Equals(st1) || tab1.sring.Equals(st2) select tab1;
                        //var query2 = from tab1 in ent.before_stemmer where tab1.term == search_arr[i+1] select tab1;

                        tock.Add(query1.ToList());

                        for (int k = 0; k < tock[i].Count - 1; k++)
                        {

                            if (tock[i][k].doc_no != tock[i][k + 1].doc_no)
                                continue;
                            string[] pos1 = tock[i][k].position.Split(',');
                            string[] pos2 = tock[i][k + 1].position.Split(',');
                            int dif = 0;
                            int[] x1 = Array.ConvertAll(pos1, s1 => int.Parse(s1));
                            int[] x2 = Array.ConvertAll(pos2, s1 => int.Parse(s1));
                            int coun1 = 0;
                            int count2 = 0;

                            while (true)
                            {
                                if (Math.Abs(x1[coun1] - x2[count2]) == 1)
                                {
                                    int doc = (int)tock[i][k].doc_no;

                                    var query2 = from tab2 in ent.crawler_data where tab2.id == doc select tab2;
                                    List<crawler_data> c = new List<crawler_data>(query2.ToList());

                                    if (i > 0 && final_r.Contains(c[0].URL))
                                    {
                                        r.Add(c[0].URL);
                                        break;
                                    }
                                    else if (i > 0 && !final_r.Contains(c[0].URL))
                                    {
                                        break;
                                    }
                                    else if (i == 0 && !r.Contains(c[0].URL))
                                    {
                                        r.Add(c[0].URL);
                                        break;
                                    }
                                    
                                    else break;
                                    
                                }

                                int c1 = coun1 + 1;
                                int c2 = count2 + 1;
                                if (x1[coun1] > x2[count2] && c2 < x2.Length)
                                    count2++;
                                else if (x1[coun1] < x2[count2] && c1 < x1.Length)
                                    coun1++;
                                else if (x1[coun1] > x2[count2] && c2 == x2.Length)
                                    break;
                                else if (x2[count2] > x1[coun1] && c1 == x1.Length)
                                    break;
                                
                            }
                        }
                        final_r = new HashSet<string>(r);
                        r.Clear();

                        //if (phrasecount == r.Count)
                        //{
                        //    r.Clear();
                        //    break;
                        //}

                        //phrasecount = r.Count;



                    }
                }
                else
                {
                    string st1 = arrayStemmer[0];

                    var query1 = from tab1 in ent.tockenzs where tab1.sring.Equals(st1) select tab1;
                    //var query2 = from tab1 in ent.before_stemmer where tab1.term == search_arr[i+1] select tab1;

                    tock.Add(query1.ToList());

                    for (int k = 0; k < tock[0].Count; k++)
                    {
                        int doc = (int)tock[0][k].doc_no;

                        var query2 = from tab2 in ent.crawler_data where tab2.id == doc select tab2;
                        List<crawler_data> c = new List<crawler_data>(query2.ToList());


                        r.Add(c[0].URL);

                    }
                    List<results> final_res2 = new List<results>();
                    for (int j = 0; j < r.Count; j++)
                    {
                        results g = new results();
                        g.links = r.ElementAt(j);
                        final_res2.Add(g);
                    }
                    return View(final_res2);

                }
                List<results> final_res1 = new List<results>();
                for (int j = 0; j < final_r.Count; j++)
                {
                    results g = new results();
                    g.links = final_r.ElementAt(j);
                    final_res1.Add(g);
                }
                    return View(final_res1);

            }
            /////////////////////////////////////Multi Search ////////////////////////////////////////////////////////////
            else
            {

                string[] search_arr = search_txt.Split(' ');
                string[] arrayStemmer = stemmer.GetSteamWords(search_arr);
                
                if (arrayStemmer.Length > 1)
                {
                    List<List<tockenz>> tock = new List<List<tockenz>>();
                    HashSet<string> oneword = new HashSet<string>();
                    Dictionary<int, int> multisearch = new Dictionary<int, int>();


                    for (int i = 0; i < arrayStemmer.Length - 1; i++)
                    {

                        string st1 = arrayStemmer[i];
                        string st2 = arrayStemmer[i + 1];
                        var query1 = from tab1 in ent.tockenzs where tab1.sring.Equals(st1) || tab1.sring.Equals(st2) select tab1;
                        //var query2 = from tab1 in ent.before_stemmer where tab1.term == search_arr[i+1] select tab1;

                        tock.Add(query1.ToList());
                        multisearch.Clear();
                        for (int k = 0; k < tock[i].Count - 1; k++)
                        {

                            if (tock[i][k].doc_no != tock[i][k + 1].doc_no)
                            {
                                int doc = (int)tock[i][k].doc_no;

                                var query2 = from tab2 in ent.crawler_data where tab2.id == doc select tab2;
                                List<crawler_data> c = new List<crawler_data>(query2.ToList());


                                if (!r.Contains(c[0].URL) && !oneword.Contains(c[0].URL))
                                    oneword.Add(c[0].URL);
                                continue;
                            }
                            string[] pos1 = tock[i][k].position.Split(',');
                            string[] pos2 = tock[i][k + 1].position.Split(',');
                            int dif = 0;
                            int[] x1 = Array.ConvertAll(pos1, s1 => int.Parse(s1));
                            int[] x2 = Array.ConvertAll(pos2, s1 => int.Parse(s1));
                            int coun1 = 0;
                            int count2 = 0;
                            int min = 9999;
                            
                            while (true)
                            {
                                if (Math.Abs(x1[coun1] - x2[count2]) == 1)
                                {
                                    int doc = (int)tock[i][k].doc_no;

                                    var query2 = from tab2 in ent.crawler_data where tab2.id == doc select tab2;
                                    List<crawler_data> c = new List<crawler_data>(query2.ToList());

                                    r.Add(c[0].URL);
                                    break;
                                }
                                else if (Math.Abs(x1[coun1] - x2[count2]) > 1)
                                {
                                    if (min > Math.Abs(x1[coun1] - x2[count2]))
                                        min = Math.Abs(x1[coun1] - x2[count2]);

                                }

                                int c1 = coun1 + 1;
                                int c2 = count2 + 1;
                                if (x1[coun1] > x2[count2] && c2 < x2.Length)
                                    count2++;
                                else if (x1[coun1] < x2[count2] && c1 < x1.Length)
                                    coun1++;
                                else if (x1[coun1] > x2[count2] && c2 == x2.Length)
                                {
                                    multisearch.Add((int)tock[i][k].doc_no, min);
                                    break;
                                }
                                else if (x2[count2] > x1[coun1] && c1 == x1.Length) 
                                {
                                    multisearch.Add((int)tock[i][k].doc_no, min);
                                    break;
                                }

                                //else
                                //{
                                //    multisearch.Add((int)tock[i][k].doc_no, min);
                                //    break;
                                //}


                            }
                            



                        }

                        var items = new Dictionary<int, int>(multisearch);


                        //from pair in multisearch
                        //            orderby pair.Value ascending
                        //            select pair;

                        List<int> temp = new List<int>();

                        foreach (var item in items.OrderBy(j => j.Value))
                        {

                            temp.Add(item.Key);
                        }



                        for (int l = 0; l < temp.Count; l++)
                        {
                            int doc = (int)temp[l];
                            var query2 = from tab2 in ent.crawler_data where tab2.id == doc select tab2;
                            List<crawler_data> c = new List<crawler_data>(query2.ToList());

                            r.Add(c[0].URL);

                        }

                        foreach (var item in oneword)
                            r.Add(item);




                    }

                }
                else
                {
                    List<List<tockenz>> tock = new List<List<tockenz>>();
                    string st1 = arrayStemmer[0];

                    var query1 = from tab1 in ent.tockenzs where tab1.sring.Equals(st1) select tab1;
                    //var query2 = from tab1 in ent.before_stemmer where tab1.term == search_arr[i+1] select tab1;

                    tock.Add(query1.ToList());

                    for (int k = 0; k < tock[0].Count; k++)
                    {
                        int doc = (int)tock[0][k].doc_no;

                        var query2 = from tab2 in ent.crawler_data where tab2.id == doc select tab2;
                        List<crawler_data> c = new List<crawler_data>(query2.ToList());


                        r.Add(c[0].URL);



                    }
                }
            }
            List<results> final_res = new List<results>();
            for (int j = 0; j < r.Count; j++) {
                results g=new results();
                g.links = r.ElementAt(j);
                final_res.Add(g);
            
            }
           

            return View(final_res);
        }
        
        public ActionResult website()
        {


            return View();
        }

    }
}
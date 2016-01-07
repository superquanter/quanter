using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Threading;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quanter.Trader.Connector
{
    public class ThsStockTrader : BaseStockTrader, IStockTrader
    {
        const int MDI_FRAME = 0xE900;

        const int CMD_BUY = 161;
        const int CMD_SELL = 162;
        const int CMD_QUERY = 165;
        const int CMD_TODAY_TRANSACTION = 167;
        const int CMD_TODAY_ENTRUST = 168;

        public ThsStockTrader() { }

        IntPtr hWnd;    // 窗口句柄

        /// <summary>
        /// 获取右侧主面板句柄
        /// </summary>
        /// <returns></returns>
        private IntPtr GetDetailPanel()
        {
            const int PANEL_DLG = 0xE901;
            IntPtr h1 = Win32API.GetDlgItem(hWnd, MDI_FRAME);
            h1 = Win32API.GetDlgItem(h1, PANEL_DLG);

            return h1;
        }

        /// <summary>
        /// 获取持仓列表信息控件
        /// </summary>
        /// <returns></returns>
        private IntPtr GetPositonList()
        {
            const int HEXIN_SCROLL_WND = 0x0417;
            const int HEXIN_SCROLL_WND_2 = 0x00C8;
            const int LIST_VIEW = 0x0417;

            IntPtr h1 = GetDetailPanel();
            Thread.Sleep(1000);
            h1 = Win32API.GetDlgItem(h1, HEXIN_SCROLL_WND);
            h1 = Win32API.GetDlgItem(h1, HEXIN_SCROLL_WND_2);
            h1 = Win32API.GetDlgItem(h1, LIST_VIEW);

            return h1;
        }

        #region 点击各个功能菜单

        private void ClickSellTreeViewItem()
        {
            SendCommand(CMD_SELL);
        }

        private void ClickBuyTreeViewItem()
        {
            SendCommand(CMD_BUY);
        }

        public void SendCommand(int cmd)
        {
            Win32API.SendMessage(hWnd, Win32Code.WM_COMMAND, cmd, 0);
            Thread.Sleep(50);
        }

        private void ClickQueryTreeViewItem()
        {
            SendCommand(CMD_QUERY);
        }

        private void ClickQueryDrwtTreeViewItem()
        {
            SendCommand(CMD_TODAY_ENTRUST);
        }

        private void ClickQueryDrcjTreeViewItem()
        {
            SendCommand(CMD_TODAY_TRANSACTION);
        }

        #endregion


        #region 接口实现

        //WebStockTrader wst = null;
        /// <summary>
        /// 检测
        /// </summary>
        public override void Init()
        {
            hWnd = Win32API.FindWindow(null, @"网上股票交易系统5.0");
        }

        protected override TraderResult internalSellStock(string code, float price, int amount)
        {
            ClickSellTreeViewItem();

            const int ID_TXT_CODE = 0x0408;
            const int ID_TXT_PRICE = 0x0409;
            const int ID_TXT_NUM = 0x040A;
            const int ID_BTN_SELL = 0x3EE;

            // 设定代码,价格,数量
            IntPtr hSellPanel = GetDetailPanel();
            Win32API.SendDlgItemMessage(hSellPanel, ID_TXT_CODE, Win32Code.WM_SETTEXT, 0, code);
            PeekAndDelay(50);
            Win32API.SendDlgItemMessage(hSellPanel, ID_TXT_PRICE, Win32Code.WM_SETTEXT, 0, price.ToString());
            Win32API.SendDlgItemMessage(hSellPanel, ID_TXT_NUM, Win32Code.WM_SETTEXT, 0, amount.ToString());

            // 点击卖出按钮
            Win32API.PostMessage(hSellPanel, Win32Code.WM_COMMAND, ID_BTN_SELL, 0);

            int no = waitAndGetTradeID(hSellPanel, code, price, amount);

            TraderResult result = new TraderResult();
            result.Code = TraderResultEnum.SUCCESS;
            result.EntrustNo = no;
            return result;
        }

        protected override TraderResult internalBuyStock(string code, float price, int amount)
        {
            const int ID_TXT_CODE = 0x0408;
            const int ID_TXT_PRICE = 0x0409;
            const int ID_TXT_NUM = 0x040A;
            const int ID_BTN_BUY = 0x3EE;

            ClickBuyTreeViewItem();

            // 设定代码,价格,数量
            IntPtr hBuySell = GetDetailPanel();
            Win32API.SendDlgItemMessage(hBuySell, ID_TXT_CODE, Win32Code.WM_SETTEXT, 0, code);
            PeekAndDelay(50);
            Win32API.SendDlgItemMessage(hBuySell, ID_TXT_PRICE, Win32Code.WM_SETTEXT, 0, price.ToString());
            Win32API.SendDlgItemMessage(hBuySell, ID_TXT_NUM, Win32Code.WM_SETTEXT, 0, amount.ToString());

            // 点击买入按钮
            Win32API.PostMessage(hBuySell, Win32Code.WM_COMMAND, ID_BTN_BUY, 0);

            int no = waitAndGetTradeID(hBuySell, code, price, amount);

            TraderResult result = new TraderResult();
            result.Code = TraderResultEnum.SUCCESS;
            result.EntrustNo = no;
            return result;
        }

        protected override TraderResult internalCancelStock(int entrustNo)
        {
            return null;
            //return wst.CancelStock(entrustNo);
            // ClickCancelTreeViewItem();
        }

        protected override void internalKeep()
        {
            // 刷新
            Win32API.SendMessage(hWnd, Win32Code.WM_KEYDOWN, Win32Code.VK_F4, 0);
        }

        private IntPtr findWndClass(IntPtr hWnd, IntPtr child)
        {
            IntPtr hc = Win32API.FindWindowEx(hWnd, child, "CLIPBRDWNDCLASS", null);
            STRINGBUFFER sb;
            Win32API.GetClassName(hc, out sb, 15);
            if (sb.szText.ToUpper().Equals("CLIPBRDWNDCLASS"))
            {
                MessageBox.Show(sb.szText);
            }
            else
            {
                findWndClass(hWnd, hc);
            }
            Console.WriteLine(sb.szText);

            return hc;
        }

        protected override TraderResult internalGetTradingAccountInfo()
        {
            //ClickQueryTreeViewItem();

            //IntPtr list = GetPositonList();

            //String s = readListData(list);
            //String[] ps = s.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            //TradingAccount account = new TradingAccount();
            //GetStocks(account, ps);

            //IntPtr hPanel = GetDetailPanel();
            //Win32API.SendMessage(hPanel, Win32Code.WM_COMMAND, 32790, 0);   // refresh

            //TradingAccount.FundInfo fund = new TradingAccount.FundInfo();
            //account.fundInfo = fund;
            //fund.MoneyName = "人民币";

            //account.fundInfo.CurrentBalance = GetDlgItemPrice(hPanel, 0x03F4);
            //account.fundInfo.EnableBalance = GetDlgItemPrice(hPanel, 0x03F8);
            //account.fundInfo.FetchBalance = GetDlgItemPrice(hPanel, 0x03F9);
            //account.fundInfo.AssetBalance = GetDlgItemPrice(hPanel, 0x03F7);
            //account.fundInfo.MarketValue = GetDlgItemPrice(hPanel, 0x03F6);

            //TraderResult result = new TraderResult();
            //result.Code = TraderResultEnum.SUCCESS;
            //result.Result = account;

            //return result;
            return null;

        }

        protected override TraderResult internalGetTodayTradeList()
        {
            //ClickQueryDrcjTreeViewItem();
            //IntPtr list = GetPositonList();

            //String s = readListData(list);

            //String[] ps = s.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //List<TransactionRecord> records = GetTransactionList(ps);

            //TraderResult result = new TraderResult();
            //result.Code = TraderResultEnum.SUCCESS;
            //result.Result = records;

            //return result;

            return null;
        }

        protected override TraderResult internalGetTodayEntrustList()
        {
            return base.internalGetTodayEntrustList();
        }

        #endregion

        private void lookupTurnoverReturn()
        {
            while (true)
            {
                if (true)  // 发现回报
                {
                    base.StockTrader_OnTurnOverReturn(0, "000", 0, 0);
                }
            }
        }

        //private List<EntrustRecord> GetEntrustList(String[] ps)
        //{
        //    List<EntrustRecord> records = new List<EntrustRecord>();
        //    for (int i = 1; i < ps.Length; i++)
        //    {
        //        Console.WriteLine(ps[i]);
        //        String[] stocks = ps[i].Split(new string[] { "\t" }, StringSplitOptions.None);
        //        EntrustRecord rec = new EntrustRecord
        //        {
        //            Date = DateTime.Parse(stocks[0] + " " + stocks[6]),
        //            StockCode = stocks[1],
        //            StockName = stocks[2],
        //            OperType = stocks[3],
        //            Price = float.Parse(stocks[4]),
        //            Amount = int.Parse(stocks[5]),
        //            TransactAmount = int.Parse(stocks[7]),
        //            TransactPrice = float.Parse(stocks[8]),
        //            No = int.Parse(stocks[1]),
        //        };
        //        records.Add(rec);
        //    }

        //    return records;
        //}

        //private List<TransactionRecord> GetTransactionList(String[] ps)
        //{
        //    List<TransactionRecord> records = new List<TransactionRecord>();
        //    for (int i = 1; i < ps.Length; i++)
        //    {
        //        Console.WriteLine(ps[i]);
        //        String[] stocks = ps[i].Split(new string[] { "\t" }, StringSplitOptions.None);
        //        TransactionRecord rec = new TransactionRecord
        //        {
        //            Date = DateTime.ParseExact(stocks[0], "yyyyMMdd", null),
        //            EntrustNo = int.Parse(stocks[1]),
        //            StockCode = stocks[2],
        //            StockName = stocks[3],
        //            OperType = stocks[4],
        //            Amount = int.Parse(stocks[5]),
        //            Price = float.Parse(stocks[6]),
        //            Turnover = float.Parse(stocks[7]),
        //            TransactionNo = int.Parse(stocks[10])
        //        };
        //        records.Add(rec);
        //    }

        //    return records;
        //}

        //private void GetStocks(TradingAccount account, String[] ps)
        //{
        //    for (int i = 1; i < ps.Length; i++)
        //    {
        //        String[] stocks = ps[i].Split(new string[] { "\t" }, StringSplitOptions.None);
        //        StockHolderInfo shi = new StockHolderInfo
        //        {
        //            StockCode = stocks[0],
        //            StockName = stocks[1],
        //            CurrentAmount = int.Parse(stocks[2]),
        //            EnableAmount = int.Parse(stocks[3]),
        //            IncomeAmount = int.Parse(stocks[4]),
        //            CostPrice = float.Parse(stocks[5]),
        //            KeepCostPrice = float.Parse(stocks[6]),
        //            LastPrice = float.Parse(stocks[7]),
        //            MarketValue = float.Parse(stocks[10]),
        //            ExchangeName = stocks[11],
        //            StockAccount = stocks[12]
        //        };
        //        account.AddStockHolder(shi);
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWndTrade"></param>
        /// <param name="code"></param>
        /// <param name="price"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private int waitAndGetTradeID(IntPtr hWndTrade, String code, float price, int amount)
        {
            const int ID_TXT_CODE = 0x0408;
            const int ID_TXT_PRICE = 0x0409;
            const int ID_TXT_NUM = 0x040A;

            uint dwTick = Win32API.GetTickCount();
            int entrustNo = 0;

            #region callback
            /*
                        Win32API.EnumWindowsProc EnumWindowsProc = delegate (IntPtr hTip, int lParam)
                        {
                            String clazz = GetClassName(hTip);

                            // 不是对话框的基类,继续扫描
                            if ("#32770" != clazz)
                                return true;

                            // 不含有0x0555 组件，就不用管
                            IntPtr hCaption = Win32API.GetDlgItem(hTip, 0x0555);
                            if (hCaption == IntPtr.Zero)
                                return true;

                            //
                            IntPtr hCap = Win32API.GetDlgItem(hTip, 0x0555);
                            String sCap = GetWindowText(hCap, 10);
                            switch (sCap.Substring(0, 1))
                            {
                                case "委":
                                    Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0x0006, 0);
                                    break;
                                case "提":
                                    if ("提示" == sCap)
                                    {
                                        String s = GetDlgItemTextEx(hTip, 0x03EC);
                                        Console.WriteLine("提示内容：" + s);
                                        if (s.Contains("成功提交"))
                                        {
                                            int i = s.IndexOf("。");
                                            entrustNo = int.Parse(s.Substring(17, i - 17));

                                            Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);
                                            return false;
                                        }
                                        else if (s == "请输入委托价格")
                                        {
                                            Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);

                                            Win32API.SendDlgItemMessage(hWndTrade, 0x0409, Win32Code.WM_SETTEXT, 0, price.ToString());
                                            Win32API.PostMessage(hWndTrade, Win32Code.WM_COMMAND, 0x03EE, 0);
                                        }
                                        else if (s == "请输入委托数量")
                                        {
                                            Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);

                                            Win32API.SendDlgItemMessage(hWndTrade, 0x040A, Win32Code.WM_SETTEXT, 0, amount.ToString());
                                            Win32API.PostMessage(hWndTrade, Win32Code.WM_COMMAND, 0x03EE, 0);
                                        }
                                        else if (s == "提交失败")
                                        {
                                            Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);
                                            entrustNo = 0;
                                            return false;
                                        }
                                        else if (s == "正在连接委托主站...")
                                        {
                                            PeekAndDelay(1000);
                                        }
                                        else if (s == "正在验证用户身份...")
                                        {
                                            PeekAndDelay(1000);
                                        }
                                        else
                                        {
                                            Console.WriteLine("未获取到值");
                                        }
                                    }
                                    else if ("提示信息" == sCap)
                                    {
                                        // 委托价格不在  
                                        // TODO: 确定0x006是否正确
                                        Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0006, 0);
                                    }
                                    break;
                                default:
                                    break;
                            }

                            return true;
                        };
                        */
            #endregion callback

            while (true)
            {
                if (Win32API.GetTickCount() - dwTick > 3 * 1000 || entrustNo != 0)
                {
                    break;
                }
                PeekAndDelay(40);
                IntPtr hTip = IntPtr.Zero;
                // IntPtr hTip = Win32API.EnumWindows(EnumWindowsProc, ref ptr);
                Win32API.EnumWindows(enumTipWindowProc, ref hTip);

                // 获取窗口标题
                IntPtr hCaption = Win32API.GetDlgItem(hTip, 0x0555);
                if (hCaption == IntPtr.Zero)
                    continue;

                String sCap = GetWindowText(hCaption, 10);
                switch (sCap.Substring(0, 1))
                {
                    case "委":
                        Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0x0006, 0);
                        break;
                    case "提":
                        if ("提示" == sCap)
                        {
                            String s = GetDlgItemTextEx(hTip, 0x03EC);
                            Console.WriteLine("提示内容：" + s);
                            if (s.Contains("成功提交"))
                            {
                                int i = s.IndexOf("。");
                                entrustNo = int.Parse(s.Substring(17, i - 17));

                                Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);

                                return entrustNo;
                            }
                            else if (s == "请输入委托价格")
                            {
                                Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);

                                Win32API.SendDlgItemMessage(hWndTrade, 0x0409, Win32Code.WM_SETTEXT, 0, price.ToString());
                                Win32API.PostMessage(hWndTrade, Win32Code.WM_COMMAND, 0x03EE, 0);
                            }
                            else if (s == "请输入委托数量")
                            {
                                Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);

                                Win32API.SendDlgItemMessage(hWndTrade, 0x040A, Win32Code.WM_SETTEXT, 0, amount.ToString());
                                Win32API.PostMessage(hWndTrade, Win32Code.WM_COMMAND, 0x03EE, 0);
                            }
                            else if (s == "提交失败")
                            {
                                Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);
                                entrustNo = 0;
                                return entrustNo;
                            }
                            else if (s == "正在连接委托主站...")
                            {
                                //LogHelper.Instance.WriteLog(null, s, true);
                                PeekAndDelay(1000);
                            }
                            else if (s == "正在验证用户身份...")
                            {
                                //LogHelper.Instance.WriteLog(null, s, true);
                                PeekAndDelay(1000);
                            }
                            else if (s.StartsWith("提交失败"))
                            {
                                Console.WriteLine("发生提交失败{0}", s);
                                Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0002, 0);

                                Win32API.SendDlgItemMessage(hWndTrade, ID_TXT_CODE, Win32Code.WM_SETTEXT, 0, code);
                                PeekAndDelay(50);
                                Win32API.SendDlgItemMessage(hWndTrade, ID_TXT_PRICE, Win32Code.WM_SETTEXT, 0, price.ToString());
                                Win32API.SendDlgItemMessage(hWndTrade, ID_TXT_NUM, Win32Code.WM_SETTEXT, 0, amount.ToString());
                                Win32API.PostMessage(hWndTrade, Win32Code.WM_COMMAND, 0x03EE, 0);

                            }
                        }
                        else if ("提示信息" == sCap)
                        {
                            // 委托价格不在  
                            // TODO: 确定0x006是否正确
                            Win32API.SendMessage(hTip, Win32Code.WM_COMMAND, 0X0006, 0);
                        }
                        break;
                    default:
                        break;
                }
            }
            return entrustNo;

        }

        private bool enumTipWindowProc(IntPtr hTip, ref IntPtr lParam)
        {
            String clazz = GetClassName(hTip);

            // 不是对话框的基类,继续扫描
            if ("#32770" != clazz)
                return true;

            //// 是否处于活动状态
            //// 非活动状态的不处理
            //LONG wndStype = GetWindowLong(hWndChild, GWL_STYLE);
            //if ((wndStype & WS_VISIBLE) == 0)
            //    return TRUE;

            // 不含有0x0555 组件，就不用管
            IntPtr hCaption = Win32API.GetDlgItem(hTip, 0x0555);
            if (hCaption == IntPtr.Zero)
                return true;

            String sCap = GetWindowText(hCaption, 10);
            switch (sCap.Substring(0, 1))
            {
                case "委":
                    if ("委托确认" == sCap)
                    {
                        lParam = hTip;
                        return false;
                    }
                    break;
                case "提":
                    if ("提示" == sCap || "提示信息" == sCap)
                    {
                        lParam = hTip;
                        return false;
                    }
                    break;
                default:
                    break;
            }

            return true;
        }
        private String readListData(IntPtr hwnd)
        {
            uint dwTick = Win32API.GetTickCount();
            String s = "";
            while (true)
            {
                // 发送复制命令
                Win32API.SendMessage(hwnd, Win32Code.WM_COMMAND, 57634, 0);
                // 延时等对方处理
                PeekAndDelay(40);
                s = Clipboard.GetText();
                if (s != null && s.Length != 0) break;

                // 是否超时未响应
                if (Win32API.GetTickCount() - dwTick > 3000)
                    return "";
            }

            return s;
        }

        private String GetWindowText(IntPtr hWnd, int len)
        {
            STRINGBUFFER sb;
            Win32API.GetWindowText(hWnd, out sb, len);

            return sb.szText;
        }

        private float GetDlgItemPrice(IntPtr hWnd, int dlgId)
        {
            IntPtr h = Win32API.GetDlgItem(hWnd, dlgId);
            string s = GetWindowText(h, 10);

            if (s == null || s.Length == 0) return 0;

            return float.Parse(s);
        }

        private String GetDlgItemText(IntPtr hWnd, int dlgId)
        {
            IntPtr h = Win32API.GetDlgItem(hWnd, dlgId);
            STRINGBUFFER sb;
            Win32API.GetWindowText(hWnd, out sb, 100);

            return sb.szText;
        }
        private String GetDlgItemTextEx(IntPtr hWnd, int dlgId)
        {
            IntPtr h = Win32API.GetDlgItem(hWnd, dlgId);
            //STRINGBUFFER sb;
            //Win32API.GetWindowText(hWnd, out sb, 100);

            byte[] lParamStr = new byte[120];
            Win32API.SendMessage(h, Win32Code.WM_GETTEXT, 120, lParamStr);
            String sText = Encoding.Unicode.GetString(lParamStr);

            return sText;
        }

        private void PeekAndDelay(int delay)
        {
            uint dwTick = Win32API.GetTickCount();
            MSG msg = new MSG();
            while (Win32API.GetTickCount() - dwTick < delay)
            {
                if (Win32API.PeekMessage(ref msg, 0, 0, 0, Win32Code.PM_REMOVE))
                {
                    Win32API.TranslateMessage(ref msg);
                    Win32API.DispatchMessage(ref msg);
                }
                Thread.Sleep(1);
            }
        }

        private String GetClassName(IntPtr hWnd)
        {
            STRINGBUFFER sb;
            Win32API.GetClassName(hWnd, out sb, 40);

            return sb.szText;
        }
    }
}

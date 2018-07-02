﻿using PLCReadWrite;
using PLCReadWrite.PLCControl;
using PLCReadWrite.PLCControl.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCReadWriteDemo
{
    class Program
    {
        private static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        static void Main(string[] args)
        {
            //PLCCollectionTest();

            StringPLCCollectionTest();

            Console.ReadKey();
        }

        private static void PLCCollectionTest()
        {
            var collection = new PLCDataCollection<short>("温度数据集合");
            sw.Restart();
            collection.Add("Slot1", "D100", 10);
            collection.Update();
            sw.Stop();
            Console.WriteLine("Elapsed.TotalMilliseconds:{0}", sw.Elapsed.TotalMilliseconds);

            //foreach (var item in collection)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            Console.WriteLine("*************************************");

            IPLC plc = new MelsecPlcA1E("192.168.100.1", 5000);
            PLCReadWrite.PLCControl.PLCControl plcControl = new PLCReadWrite.PLCControl.PLCControl(plc);
            plcControl.SetPersistentConnection();
            plcControl.AddCollection(0, collection);

            sw.Restart();
            var data = plcControl.GetCollection<short>(0);
            plcControl.ReadCollection(ref data);
            sw.Stop();
            Console.WriteLine("ReadCollection Elapsed.TotalMilliseconds:{0}", sw.Elapsed.TotalMilliseconds);

            //for (int i = 0; i < 20; i++)
            //{
            //    sw.Restart();
            //    plcControl.ReadCollection(ref data);
            //    sw.Stop();
            //    Console.WriteLine("Elapsed.TotalMilliseconds:{0}", sw.Elapsed.TotalMilliseconds);
            //}

            //foreach (var item in collection)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            Console.WriteLine("*************************************");
        }

        private static void StringPLCCollectionTest()
        {
            var collection = new PLCDataCollection("温度数据集合");
            sw.Restart();
            collection.Add("Slot1", "D1000", DataType.Int16Address, 100);
            collection.Update();
            sw.Stop();
            Console.WriteLine("Elapsed.TotalMilliseconds:{0}", sw.Elapsed.TotalMilliseconds);

            //foreach (var item in collection)
            //{
            //    Console.WriteLine(item.ToString());
            //}

            Console.WriteLine("*************************************");

            IPLC plc = new MelsecPlcA1E("127.0.0.1", 5000);
            PLCReadWrite.PLCControl.String.PLCControl plcControl = new PLCReadWrite.PLCControl.String.PLCControl(plc);
            plcControl.SetPersistentConnection();

            if (plcControl.Open())
            {
                Console.WriteLine("Opened!!!!!!!!!!");
            }

            //sw.Restart();
            //plcControl.AddCollection(0, collection);
            //sw.Stop();
            //Console.WriteLine("AddCollection Elapsed.TotalMilliseconds:{0}", sw.Elapsed.TotalMilliseconds);

            //sw.Restart();
            //var data = plcControl.GetCollection(0);
            //sw.Stop();


            for (int i = 0; i < 20; i++)
            {
                sw.Restart();
                plcControl.ReadCollection(ref collection);
                sw.Stop();
                Console.WriteLine("Elapsed.TotalMilliseconds:{0}", sw.Elapsed.TotalMilliseconds);
            }

            //foreach (var item in collection)
            //{
            //    Console.WriteLine(item.ToString());
            //}
            Console.WriteLine("*************************************");
        }


        /*
            read.IsSuccess = true;
            read.Content = new byte[]
            {
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
                0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,
            };
         */
    }
}

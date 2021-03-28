using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;


  /// <summary>
  /// Logging class
  /// Alex McCown
  /// 11/22/2020
  /// </summary>
  class Logging
  {
      int export_level;
      /*
       * 0 - Errors only, anything at 0 will export to the log
       * 1 - Warnings, anything at level 1 and below will export to the log
       * 2 - Info, anything at level 2 and below will export to the log
       * 3 - Stage, anything at level 3 and below will export to the log
       * 4 - Timing and other information, anything at level 4 and below will export to the log
       */
      private String log_file_name;
      private List<String> log;
      public Stopwatch sw;
      private bool include_data_and_time;

      public Logging(int export_level, string log_file_name = "log.txt", bool include_data_and_time = true)
      {
          this.export_level = export_level;
          this.log_file_name = log_file_name;
          this.log = new List<string>();
          sw = new Stopwatch();
          this.include_data_and_time = include_data_and_time;
          if (File.Exists(log_file_name))
              File.Delete(log_file_name);
          File.WriteAllText(log_file_name, "Logging started " + DateTime.Now.ToString() + "\n");
      }

      /// <summary>
      /// Prints input string to file.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="message"> The string you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToFile(String message, int level)
      {
          if (export_level >= level)
          {
              if (include_data_and_time)
                  message = message.Insert(0, DateTime.Now.ToString() + ": ");
              message += '\n';
              File.AppendAllText(log_file_name, message);
              return true;
          }
          return false;
      }

      /// <summary>
      /// Prints input string array to file.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="message"> The string array you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToFile(String[] message, int level)
      {
          if (export_level >= level)
          {
              if (include_data_and_time)
                  File.AppendAllText(log_file_name, DateTime.Now.ToString() + ": \n");
              File.AppendAllLines(log_file_name, message);
              File.AppendAllText(log_file_name, "\n");
              return true;
          }
          return false;
      }

      /// <summary>
      /// Prints input string list to file.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="message"> The string array you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToFile(List<String> message, int level)
      {
          if (export_level >= level)
          {
              if (include_data_and_time)
                  File.AppendAllText(log_file_name, DateTime.Now.ToString() + ": \n");
              File.AppendAllLines(log_file_name, message);
              File.AppendAllText(log_file_name, "\n");
              return true;
          }
          return false;
      }

      /// <summary>
      /// Prints input exception message to file.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="exception"> The exception you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToFile(Exception exception, int level)
      {
          if (export_level >= level)
          {
              String message = exception.ToString();
              if (include_data_and_time)
                  message = message.Insert(0, DateTime.Now.ToString() + ": ");
              message += '\n';
              File.AppendAllText(log_file_name, message);
              return true;
          }
          return false;
      }

      /*
       * Logging to lists
       */


      /// <summary>
      /// Prints input string to a list.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="message"> The string you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToList(String message, int level)
      {
          if (export_level >= level)
          {
              if (include_data_and_time)
                  message = message.Insert(0, DateTime.Now.ToString() + ": ");
              log.Add(message);
              return true;
          }
          return false;
      }

      /// <summary>
      /// Prints input string array to a list.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="message"> The string array you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToList(String[] message, int level)
      {
          if (export_level >= level)
          {
              if (include_data_and_time)
                  log.Add(DateTime.Now.ToString() + ": ");
              foreach(String str in message)
                  log.Add(str);
              return true;
          }
          return false;
      }

      /// <summary>
      /// Prints input string list to a list.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="message"> The string array you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToList(List<String> message, int level)
      {
          if (export_level >= level)
          {
              if (include_data_and_time)
                  log.Add(DateTime.Now.ToString() + ": ");
              foreach (String str in message)
                  log.Add(str);
              return true;
          }
          return false;
      }

      /// <summary>
      /// Prints input exception message to a list.
      /// Recomended levels
      /// 0 - errors, 1 - warnings, 2 - info, 3 - stage, 4 - timing and other info
      /// </summary>
      /// <param name="exception"> The exception you are logging</param>
      /// <param name="level"> The level of the log </param>
      /// <returns>Returns true if exeption was of the proper level to be used</returns>
      public bool ToList(Exception exception, int level)
      {
          if (export_level >= level)
          {
              String message = exception.ToString();
              if (include_data_and_time)
                  message = message.Insert(0, DateTime.Now.ToString() + ": ");
              log.Add("Exception: " + exception.InnerException + ": " + message);
              return true;
          }
          return false;
      }

      /// <summary>
      /// Gets the log list, does not clear it
      /// </summary>
      /// <returns>Returns the list </returns>
      public List<String> GetList()
      {
          return log;
      }

      /// <summary>
      /// Saves the list to a specified file location
      /// </summary>
      /// <param name="save_location"></param>
      public void SaveList(String save_location = "log.txt")
      {
          if (include_data_and_time)
              File.AppendAllText(save_location, DateTime.Now.ToString());
          File.AppendAllLines(save_location, log);
      }

      /// <summary>
      /// Clears the error list
      /// </summary>
      public void ClearList()
      {
          log.Clear();
      }
      /// <summary>
      /// Starts a timer for timer logging
      /// </summary>
      public void StartLogTimer()
      {
          sw.Start();
      }
      /// <summary>
      /// Writes the elapsed time in ms to a log file with a message
      /// does not stop or reset the timer
      /// </summary>
      /// <param name="message"> message to write</param>
      /// <param name="level"> level to write this</param>
      public bool LogTimer_ms(String message, int level)
      {
          if (export_level >= level)
          {
              StringBuilder result = new StringBuilder();
              if (include_data_and_time)
                  result.Append(DateTime.Now.ToString() + ": ");
              result.Append(message);
              result.Append(": ");
              result.Append(sw.ElapsedMilliseconds);
              result.Append(" ms.\n");
              File.AppendAllText(log_file_name, result.ToString());
              return true;
          }
          return false;
      }
      /// <summary>
      /// Writes the elapsed time in ms to a log file with a message
      /// does not stop or reset the timer
      /// </summary>
      /// <param name="message"> message to write</param>
      /// <param name="level"> level to write this</param>
      public bool LogTimer_ns(String message, int level)
      {
          if (export_level >= level)
          {
              StringBuilder result = new StringBuilder();
              if (include_data_and_time)
                  result.Append(DateTime.Now.ToString() + ": ");
              result.Append(message);
              result.Append(": ");
              long time_ns = sw.ElapsedTicks * 1_000_000_000L / Stopwatch.Frequency;
              result.Append(time_ns);
              result.Append(" ns.\n");
              File.AppendAllText(log_file_name, result.ToString());
              return true;
          }
          return false;
      }
      /// <summary>
      /// Returns elapsed ms as a long
      /// </summary>
      /// <returns>A long of the elapsed ms</returns>
      public long Get_ms()
      {
          return sw.ElapsedMilliseconds;
      }

      /// <summary>
      /// Returns elapsed time in ns as a long
      /// </summary>
      /// <returns>A long of the elapsed ns</returns>
      public long Get_ns()
      {
          return sw.ElapsedTicks * 1_000_000_000L / Stopwatch.Frequency;
      }
      /// <summary>
      /// Stops and resets the timer
      /// </summary>
      public void StopTimer()
      {
          sw.Stop();
          sw.Reset();
      }

  }

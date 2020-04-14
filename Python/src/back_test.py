import time
import pandas as pd
import numpy as np
import pyodbc

start_time = time.time()
def watch_restart(title):
  global start_time
  start_time = time.time()
  print(title)
def watch_print(title):
  global start_time
  print(title,round(time.time() - start_time, 4), 'seconds')
  print('')

connection = pyodbc.connect('Driver={SQL Server Native Client 11.0};Server=(local);Database=Sandbox;Trusted_Connection=yes;')
symbols = pd.read_sql('SELECT * FROM [Sandbox].[dbo].[SymbolMaster]', connection)

for i, symbol in symbols.iterrows():
  watch_restart('START- Id={0}, Symbol={1}'.format(symbol['Id'], symbol['Symbol']))
    
  prices = pd.read_sql('SELECT * FROM [Sandbox].[dbo].[StockPrice] WHERE SymbolId = {0}'.format(symbol['Id']), connection)

  # for idx, price in prices.iterrows():
  
      


  watch_print('STOP')




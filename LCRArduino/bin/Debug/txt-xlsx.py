import os
import xlrd
import xlwt
import xlsxwriter
import tkinter as tk
from tkinter import filedialog

root = tk.Tk()
root.withdraw()


directory = filedialog.askdirectory(parent=root,initialdir=os.path.dirname(os.path.realpath("plotter.py")),title='Please select a directory')
outPutName = "CompiledData.xlsx"
sheetCount = 0


workbook = xlsxwriter.Workbook(directory+"/"+outPutName)
new_sheet_names = []

for filename in os.listdir(directory):
	if filename.endswith(".txt"): 
		file = os.path.join(directory, filename)
		f = open(file, "r")
		row = 0

		sheetIndex = 0
		sheetCount = sheetCount + 1

		worksheet = workbook.add_worksheet(filename[:-4])
		new_sheet_names.append(filename[:-4]) #list of worksheet names for sorting later

		for line in f:
			#print(line)
			values = line.split(',')
			for col in range(0,5):
				if row == 0:
					worksheet.write(row,col,values[col])
				else:
					worksheet.write(row,col,float(values[col]))
			row = row + 1
		sheetIndex = sheetIndex + 1
		continue
	else:
		#.DS_Store and other garbage files caught here
		continue



#sort the new_sheet_names list
for i in range(sheetCount):
	for j in range(i+1,sheetCount):
		if float(new_sheet_names[i]) > float(new_sheet_names[j]):
			new_sheet_names[i],new_sheet_names[j] = new_sheet_names[j],new_sheet_names[i] 



# use the new_sheet_names list to sort
workbook.worksheets_objs.sort(key=lambda x: new_sheet_names.index(x.name))
workbook.close()
print('done')
import os
import math
import numpy as np
from numpy  import array
import xlrd
import xlwt
import xlsxwriter
import tkinter as tk
from tkinter import filedialog
import matplotlib
import matplotlib.pyplot as plt
from matplotlib.pyplot import gca
import matplotlib.ticker as mticker
import plotSettings as ps
import importlib
import sys


e0 = 0.00000000000885418782

def is_number(s):
    try:
        float(s)
        return True
    except ValueError:
        return False



def start_plotting(originFile = ""):
	try:

		#for later use in temp sweep
		tempList = np.array([]) 
		#fig, current_ax = plt.subplots() 

		#convert input to numbers
		print("Enter list of temperatures to plot (comma seperated, no spaces) to use for frequency sweep:")
		plotTemps = input("> ").split(",")
		print("Enter list of frequencies to plot (comma seperated, no spaces) to use for Temperature sweep:")
		plotFreqs = input("> ").split(",")
		for a in plotTemps:
			float(a)
		for b in plotFreqs:
			float(b)



		###### FREQUENCY SWEEP PLOTS SETUP START###########
		#Read correct pages from Worbook we just created
		#Use only the specified Tempertures in plotTemp for frequency sweep
		inBook2 = xlrd.open_workbook(originFile)
		tempPlotSheets = np.array([])
		sheetNames = inBook2.sheet_names() 
		for tempName in sheetNames:
			if tempName != "Plots": #dont add this to list of temperatures
				tempList = np.append(tempList,float(tempName))
			for temp in plotTemps:
				if tempName == temp:
					tempPlotSheets = np.append(tempPlotSheets,inBook2.sheet_by_name(tempName))
		###### FREQUENCY SWEEP PLOTS SETUP END###########

		####### TEMP SWEEP PLOTS SETUP START################

		P = [np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),
		np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),
		np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),
		np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs),np.copy(plotFreqs)]




		#go through all sheets (except plots sheet), add only relevant frequqency data
		for sheet in inBook2.sheets():
			append = [np.array([]),np.array([]),np.array([]),np.array([]),np.array([]),
					np.array([]),np.array([]),np.array([]),np.array([]),np.array([]),
					np.array([]),np.array([]),np.array([]),np.array([]),np.array([]),
					np.array([]),np.array([])]
			
			appendtand = np.array([])
			appende1 = np.array([])
			appende2 = np.array([])
			appendsig = np.array([])
			
			for row in range(2,sheet.nrows):
				for freq in plotFreqs:
					if sheet.cell_value(row, 1) == float(freq):
						append[0] = np.append(append[0],float(sheet.cell_value(row, 3)))
						append[1] = np.append(append[1],float(sheet.cell_value(row, 4)))
						append[2] = np.append(append[2],float(sheet.cell_value(row, 5)))
						append[3] = np.append(append[3],float(sheet.cell_value(row, 6)))
						append[4] = np.append(append[4],float(sheet.cell_value(row, 7)))
						append[5] = np.append(append[5],float(sheet.cell_value(row, 8)))
						append[6] = np.append(append[6],float(sheet.cell_value(row, 9)))
						append[7] = np.append(append[7],float(sheet.cell_value(row, 10)))
						append[8] = np.append(append[8],float(sheet.cell_value(row, 11)))
						append[9] = np.append(append[9],float(sheet.cell_value(row, 12)))
						append[10] = np.append(append[10],float(sheet.cell_value(row, 13)))
						append[11] = np.append(append[11],float(sheet.cell_value(row, 14)))
						append[12] = np.append(append[12],float(sheet.cell_value(row, 15)))
						append[13] = np.append(append[13],float(sheet.cell_value(row, 16)))
						append[14] = np.append(append[14],float(sheet.cell_value(row, 17)))
						append[15] = np.append(append[15],float(sheet.cell_value(row, 18)))
						append[16] = np.append(append[16],float(sheet.cell_value(row, 19)))
						#append[17] = np.append(append[0],float(sheet.cell_value(row, 20)))
				

			if append[0].size > 0:
				P[0] = np.vstack([P[0],append[0]])
			if append[1].size > 0:
				P[1] = np.vstack([P[1],append[1]])
			if append[2].size > 0:
				P[2] = np.vstack([P[2],append[2]])
			if append[3].size > 0:
				P[3] = np.vstack([P[3],append[3]])
			if append[4].size > 0:
				P[4] = np.vstack([P[4],append[4]])
			if append[5].size > 0:
				P[5] = np.vstack([P[5],append[5]])
			if append[6].size > 0:
				P[6] = np.vstack([P[6],append[6]])
			if append[7].size > 0:
				P[7] = np.vstack([P[7],append[8]])
			if append[8].size > 0:
				P[8] = np.vstack([P[8],append[8]])
			if append[9].size > 0:
				P[9] = np.vstack([P[9],append[9]])
			if append[10].size > 0:
				P[10] = np.vstack([P[10],append[10]])
			if append[11].size > 0:
				P[11] = np.vstack([P[11],append[11]])
			if append[12].size > 0:
				P[12] = np.vstack([P[12],append[12]])
			if append[13].size > 0:
				P[13] = np.vstack([P[13],append[13]])
			if append[14].size > 0:
				P[14] = np.vstack([P[14],append[14]])
			if append[15].size > 0:
				P[15] = np.vstack([P[15],append[15]])
			if append[16].size > 0:
				P[16] = np.vstack([P[16],append[16]])
			#if append[17].size > 0:
			#	P[17] = np.vstack([P[17],append[17]])


		
		####### TEMP SWEEP PLOTS SETUP END################

		ParameterDict = {
			"-1": "plot settings file",
			"0" : "Temperature (C)", #(WIP DO NO USE YET)")
			"1" : "Frequency (Hz)",
			"2" : "Log(F)",
			"3" : "Cp",
			"4" : "D", # (tan(\u03B4))")
			"5" : "Rs",
			"6" : "X",
			"7" : "Z",
			"8" : "\u03C9", #omega
			"9" : "\u03C9^2", #omega
			"10" :  "ln(\u03C9)", #omega
			"11" : "ln(\u03C9^2)",#omega
			"12" : "log(\u03C9)", #omega
			"13" : "log(\u03C9^2)", #omega
			"14" : "ln(f)",
			"15" : "\u03B5\'", #epsilon
			"16" : "\u03B5\"", #epsilon
			"17" : "\u03C1", # ro
			"18" : "\u03C3", #sigma
			"19" :"tan(\u03B4)",
		}

		keepPlotting = True;
		while keepPlotting:
			print()
			print("The following paramaters are currently available: ")
			print("-1) open plot settings file")
			print("0) "+ParameterDict.get("0"))
			print("1) "+ParameterDict.get("1"))
			print("2) "+ParameterDict.get("2"))
			print("3) "+ParameterDict.get("3"))
			print("4) "+ParameterDict.get("4")+"(tan(\u03B4))")
			print("5) "+ParameterDict.get("5"))
			print("6) "+ParameterDict.get("6"))
			print("7) "+ParameterDict.get("7"))
			print("8) "+ParameterDict.get("8")) #omega
			print("9) "+ParameterDict.get("9")) #omega
			print("10) "+ParameterDict.get("10")) #omega
			print("11) "+ParameterDict.get("11"))#omega
			print("12) "+ParameterDict.get("12")) #omega
			print("13) "+ParameterDict.get("13")) #omega
			print("14) "+ParameterDict.get("14"))
			print("15) "+ParameterDict.get("15")) #epsilon
			print("16) "+ParameterDict.get("16")) #epsilon
			print("17) "+ParameterDict.get("17")) # ro
			print("18) "+ParameterDict.get("18")) #sigma
			print("19) "+ParameterDict.get("19"))
			#print("20) y (") TBD
			print("q) QUIT")
			print()
			print()
			print("Enter plotting paramaters in form x,y or enter -1 for plot settings or q to quit")
			plotParams = input("> ")
			try: # plot number input catching
				if plotParams == "q":
					keepPlotting = False;
				elif plotParams == "-1":
					os.system("notepad plotSettings.py")
					importlib.reload(sys.modules['plotSettings'])
				else:
					xParam, yParam = int(plotParams.split(",")[0]),int(plotParams.split(",")[1])
					importlib.reload(sys.modules['plotSettings'])
					fig,ax = plt.subplots()
					if xParam == 0 and yParam >=3: #special temperature mode
						for r in range(len(plotFreqs)):
							plt.plot(tempList,P[yParam-3][1:,r].astype(np.float),'--o',label = str(P[yParam-3][0,r])+" Hz")  
							#plt.plot(tempList,e2[1:,r].astype(np.float),'--o',label = str(e2[0,r])+" Hz")  
					else: # all other plots
						for c in tempPlotSheets:
							plt.plot(c.col_values(xParam,2),c.col_values(yParam,2),linestyle=ps.LINE_STYLE,marker=ps.POINT_STYLE,markersize=ps.POINT_SIZE,linewidth=ps.LINE_WIDTH,label = str(c.cell_value(3,0)) + "\u00b0 C")  # col_values(column value,starting index)
							#plt.plot(c.col_values(xParam,2),c.col_values(yParam,2),color=ps.LINE_COLOR,linestyle=ps.LINE_STYLE,marker=ps.POINT_STYLE,markersize=ps.POINT_SIZE,linewidth=ps.LINE_WIDTH,markerfacecolor=ps.POINT_COLOR,label = str(c.cell_value(3,0)) + "\u00b0 C")  # col_values(column value,starting index)
					plt.xlabel(ParameterDict.get(str(xParam)),color=ps.X_LABEL_COLOR ,fontname=ps.X_LABEL_FONT,fontsize=ps.X_LABEL_SIZE)
					plt.ylabel(ParameterDict.get(str(yParam)),color=ps.Y_LABEL_COLOR ,fontname=ps.Y_LABEL_FONT,fontsize=ps.Y_LABEL_SIZE)
					plt.title(ParameterDict.get(str(yParam)) + " vs. " + ParameterDict.get(str(xParam)),color=ps.TITLE_LABEL_COLOR ,fontname=ps.TITLE_LABEL_FONT,fontsize=ps.TITLE_LABEL_SIZE)
					if (ps.LEGEND):
						plt.legend(prop={'family': ps.LEGEND_TEXT_FONT, 'size':ps.LEGEND_TEXT_SIZE}, loc=ps.LEGEND_LOCATION)
					plt.grid(ps.GRID)
					if ps.X_CROP:
						plt.xlim(ps.X_MIN,ps.X_MAX)
					if ps.Y_CROP:
						plt.ylim(ps.Y_MIN,ps.Y_MAX)
					a = gca()
					ax.tick_params(axis='x', direction=ps.X_TICK_DIRECTION,labelcolor=ps.X_TICK_LABEL_COLOR,color=ps.X_TICK_LINE_COLOR, labelsize=ps.X_TICK_SIZE)
					ax.tick_params(axis='y', direction=ps.Y_TICK_DIRECTION,labelcolor=ps.Y_TICK_LABEL_COLOR,color=ps.Y_TICK_LINE_COLOR, labelsize=ps.Y_TICK_SIZE)
					plt.show(block=False)
					#input()
			except:
				print("Something in your input seems wrong -- try again")
				continue	
	except:
		print("Something in your plotting parameters seems wrong -- try again")
		start_plotting(originFile)

def start_calculation(mode = 1, originFile = ""):
	print()
	if mode == 1:
		thickness = float(input("Enter Thickness of pellet (mm): ")) * math.pow(10,-3)
		diameter = float(input("Enter diameter of pellet (mm): ")) * math.pow(10,-3)
		area = math.pi*math.pow((diameter/2),2)
		########## READ #####################
		loc = originFile
		inSheets = np.array([]) # Assumng each worksheet input corroposnds to different temperature
		wb = xlrd.open_workbook(loc) 
		for x in wb.sheet_names():
			#print(str(x))
			inSheets = np.append(inSheets,wb.sheet_by_name(x))

		########## WRITE #####################
		#Create a workbook object
		outBook = xlsxwriter.Workbook(originFile[:-5]+"_Calculations.xlsx")


		#Add a sheet for each temperature(NOT FREQUENCY) in the specified Range/Interval
		outSheets = np.array([])
		for x in wb.sheet_names():
			outSheets = np.append(outSheets,outBook.add_worksheet(x))

		for outSheet in outSheets:
			outSheet.write(0,0,"Temperature(C)")
			outSheet.write(0,1,"Frequency(Hz)")
			outSheet.write(0,2,"log(f)")
			outSheet.write(0,3,"Cp")
			outSheet.write(0,4,"D")
			outSheet.write(0,5,"Rs")
			outSheet.write(0,6,"X")
			outSheet.write(0,7,"Z")
			outSheet.write(0,8,"\u03C9") #omega
			outSheet.write(0,9,"\u03C9^2") #omega
			outSheet.write(0,10,"ln(\u03C9)") #omega
			outSheet.write(0,11,"ln(\u03C9^2)") #omega
			outSheet.write(0,12,"log(\u03C9)") #omega
			outSheet.write(0,13,"log(\u03C9^2)") #omega
			outSheet.write(0,14,"ln(f)")
			outSheet.write(0,15,"\u03B5\'") #epsilon
			outSheet.write(0,16,"\u03B5\"") #epsilon
			outSheet.write(0,17,"\u03C1") # ro
			outSheet.write(0,18,"\u03C3") #sigma
			outSheet.write(0,19,"tan(\u03B4)")
			outSheet.write(0,20,"y")

		writeRow = np.ones(outSheets.size)*2 # defines starting row for all worksheets
		sheetIndex = 0 #cursor to tell us which sheet we are writing to
		currentSheet = outSheets[0]

		#startTemp = float(wb.sheet_names()[0])
		#tInterval = float(wb.sheet_names()[1]) - (float)(wb.sheet_names()[0])

		#for each row of each worksheet, read in data and insert to correct location in new worksheet
		#source file must be: Frequency(Hz) / Cp / D(Dispertion coeff) / Rs / X
		for l in range(len(inSheets)): #manipulate in all worksheets
			for i in range(1,inSheets[l].nrows):
				if is_number(inSheets[l].cell_value(i, 0)): #and (float(inSheets[l].cell_value(i, 0)) >= float(startFreq) and float(inSheets[l].cell_value(i, 0)) <= float(endFreq)): #Only read row if it contains data (numbers)
					vals = np.array([]) #Make an empty list to insert data of each row
					for j in range(inSheets[l].ncols):
						vals = np.append(vals,inSheets[l].cell_value(i, j))
					sheetIndex = l;

					currentSheet = outSheets[sheetIndex]

					tau = thickness/area
					MF = tau/e0
					currentSheet.write(int(writeRow[sheetIndex]),0,float(currentSheet.get_name())) # Write Temperature
					currentSheet.write(int(writeRow[sheetIndex]),1,vals[0]) # Write frequency
					currentSheet.write(int(writeRow[sheetIndex]),2,math.log(vals[0],10)) # Write log(f)
					currentSheet.write(int(writeRow[sheetIndex]),3,vals[1]) # Write Capacitance
					currentSheet.write(int(writeRow[sheetIndex]),4,vals[2]) # Write D = tan(delta)
					currentSheet.write(int(writeRow[sheetIndex]),5,vals[3]) # Write Rs
					currentSheet.write(int(writeRow[sheetIndex]),6,vals[4]) # Write X
					currentSheet.write(int(writeRow[sheetIndex]),7,math.pow(math.pow(vals[3],2)+math.pow(vals[4],2),0.5)) # Write Z = Sqrt(Rs^2 + X^2)
					currentSheet.write(int(writeRow[sheetIndex]),8,2*math.pi*vals[0]) # Write w = 2 * pi * f
					currentSheet.write(int(writeRow[sheetIndex]),9,math.pow(2*math.pi*vals[0],2)) # Write w^2
					currentSheet.write(int(writeRow[sheetIndex]),10,math.log(2*math.pi*vals[0])) # Write ln(w)
					currentSheet.write(int(writeRow[sheetIndex]),11,math.log(math.pow(2*math.pi*vals[0],2))) # Write ln(w^2)
					currentSheet.write(int(writeRow[sheetIndex]),12,math.log(2*math.pi*vals[0],10)) # Write log(w)
					currentSheet.write(int(writeRow[sheetIndex]),13,math.log(math.pow(2*math.pi*vals[0],2),10)) # Write log(w^2)
					currentSheet.write(int(writeRow[sheetIndex]),14,math.log(vals[0])) # Write ln(f)
					currentSheet.write(int(writeRow[sheetIndex]),15,vals[1] * MF) # Write e'
					currentSheet.write(int(writeRow[sheetIndex]),16,(vals[1] * MF)*vals[2]) # Write e''
					currentSheet.write(int(writeRow[sheetIndex]),17,(math.pow(math.pow(vals[3],2)+math.pow(vals[4],2),0.5))/tau) # Write ro
					currentSheet.write(int(writeRow[sheetIndex]),18,tau/(math.pow(math.pow(vals[3],2)+math.pow(vals[4],2),0.5))) # Write sig
					currentSheet.write(int(writeRow[sheetIndex]),19,vals[2]) #write tan(delta) = D
					#currentSheet.write(int(writeRow[sheetIndex]),20,math.log(abs(currentSheet.cell_value(writeRow[sheetIndex], 17) - currentSheet.cell_value(writeRow[sheetIndex], 17)))) # Write ro
					writeRow[sheetIndex] = writeRow[sheetIndex]+1

		#outBook.save("(Calculations)" + originFile)
		print("Done - Calculations were successful!")
		print("----------------------")
		outBook.close()


		#PLOTTING STARTS HERE
		plotQuestion = ""
		plotMode = []
		while plotQuestion != "y" and plotQuestion != "n":
			print("Do you want to plot this data? (enter y or n)")
			plotQuestion = input("> ")
			if plotQuestion == "y":
                            try:
                                start_plotting(os.path.dirname(originFile)+"/"+os.path.basename(originFile[:-5])+"_Calculations.xlsx"); #Send to plotting method
                            except:
                                print()
                                print("Cannot Create Plot")
                                print("Press Enter to QUIT")
                                print()


def convert_txt():
	root = tk.Tk()
	root.withdraw()


	directory = filedialog.askdirectory(parent=root,initialdir=os.path.dirname(os.path.realpath("plotter.py")),title='Please select a directory')
	outPutName = os.path.basename(directory) + ".xlsx"
	sheetCount = 0

	workbook = xlsxwriter.Workbook(os.path.dirname(directory)+"/"+outPutName)
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
				for col in range(0,len(values)):
					if not is_number(values[col]):
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
	print()
	print()
	print("Finished moving text files from " + os.path.basename(directory) + " folder to Excel document named " + outPutName)
	print()




	doCalculate = ""
	mode = 0 #actual mode
	res = 0 #input variable


	while doCalculate != "y" and doCalculate != "n":
		print("Do you want to run calculations on the data? (enter y or n)")
		print("NOTE: MUST BE SEPERATED FILES TO RUN CALCULATIONS")
		doCalculate = input("> ")
		if doCalculate == "y":
			while mode != -1 and mode != 1:
				print("----------------------")
				print("Enter mode of calculation -- calculation modes available (PARAMETER ORDER IMPORTANT):")
				print("1) Cp/D/Rs/X")
				print("-1) QUIT")
				res = input("> ")
				if is_number(res) and (int(res) == -1 or int(res) == 1): # check if its a number and that number is legit
					mode = int(res)
					try:
                                            start_calculation(mode, os.path.dirname(directory)+"/"+outPutName)
					except:
					    print()
					    print("Cannot perform calculation -- Try Again")
					    print("(Hint: Did you make sure files are seperated? Is Parameter order correct?)")
					    print()
					    mode = 0
				else: #otherwise, keep checking
					mode = 0
			



################################# MAIN STARTS HERE #################################

operation = ""
root = tk.Tk()
root.withdraw()


while operation != "convert" and operation != "plot":
	print("Would you like to convert text data (\"convert\") or plot data from an excel sheet? (\"plot\")?")
	operation = input("> ")
	if operation == "convert":
		convert_txt()
	elif operation == "plot":
		plot_file = filedialog.askopenfilename(parent=root,initialdir=os.path.dirname(os.path.realpath("plotter.py")),title='Please select an excel file to plot')
		start_plotting(plot_file)
	else:
		print("input not understood -- try again")
		print()







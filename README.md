

# Dielectric Analysis and Collection System (DACS)

**Table of Contents**
- [Introduction](#introduction)
- [Data Collection](#data-collection)
    * [Collection Setup](#collection-setup)
        + [Hardware](#hardware)
        + [Software](#software)
    * [Automated LCR Program](#automated-lcr-program)
- [Data Analysis](#data-analysis)
    * [Setup](#analysis-setup)
    * [Text to Excel Conversion](#text-to-excel-conversion)
    * [Plotting Your Data](#plotting-your-data)
- [Troubleshooting](#troubleshooting)
- [Future Work](#future-work)
- [Contanct](#contact)
- [Acknowledgements](#acknowledgements)
 


# Introduction
The purpose of the Dielectric Analysis and Collection System (DACS) is to provide abundant, high quality dielectric data for further study of various crystalline and ceramic materials at extreme conditions. 

This document should serve as an instruction manual for lab personnel and a showcase of the technology for the general public. For more in depth questions or comments please feel free to reach out to me, I left my contact information below.

Listed below is a high level overview of the goals I had when desinging the system and some of the key features available to the user:
- Provide extensive high quality temperature and frequency data not found in ordinary dielectric data collection methods.
- Allow for immense freedom and control in all avenues of the process, including variable frequency and temperature sweeps, slight alterations in the LCR collection parameters, calculation of a materials electrical values, quick and easy plotting, and much more.
- The system is designed to reduce the data collection and analysis/plotting time from a process taking several weeks to under one day.

There are two major components to this project: The Data Collection Stage and the Data Analysis Stage. As the names suggest, the collection stage is responsible for the automation of data collection as per the user sweep and measurement specifications. The analysis stage is used to convert this data into usable form, calculate electrical values, and finally plot, again all per the user specification. 

The two components involve different software and look and feel very different from one another, which was an intentional design decision. I highly recommend that you follow the system and procedural specifications that I have listed, as this will guarantee the best performance and as a result the best possible data.

# Data Collection

## Collection Setup


### Hardware
There are a few key pieces of hardware that we need to be functional in order for your data to be accurate and of high quality. 

- The first is the LCR meter itself. For the purposes of this document I will assume that you are using a HIOKI IM3536 Machine, although any HIOKI device should suffice. The manufacturer recommends to have the device plugged in and turned on for an hour before conducting tests, for supposed "better data". Additionally, it is recommended that the probes be compensated before conducting tests to reduce error in output values. Again, the idea is that doing this will give you "better data". In regards to how to do this -- look in the manual, I've never done it. 
![LCR Meter](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/Hardware/LCRMeter.JPG)

- Once the LCR meter is up and running out focus shifts to the furnace. The furnace should have a chamber to put in pellets or a sample holder containing the pellets, as well as a thermocouple which I will get to soon. The furnace should have its own PID controller accompanied by its own manual, but you should not have to touch it too often, as we are just using the furnace as a heat source (as in we are never directly affecting the heating element or accessing the internal furnace thermocouple).
![Furnace](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/Hardware/Furnace.JPG)

- As I mentioned earlier, the furnace chamber is where the pellet is placed into. I have a sample holder to better fasten the pellet and to prevent any potential wire shorts or other confounding conditions. The sample holder is, in essence, two metal plates, the bottom of which is adustable with a ceramic protective layer. Between these two plates, the pellet/electrode capacitor "sandwich" can safely be placed into. The holder also contains several holes for wires (in ceramic tubes for insulation) to go through and connect to to the electrodes, as well as holes for the thermocouples to comfortable sit next to the pellet and record temperature.
![Sample Holder](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/Hardware/IMG-3482.JPG)

- The Type K thermocouple(s) we are using go directly into the furnace alongside the pellets. The thermocouple works by creating a temperature dependent voltage difference, a continuous analog signal, which we convert into a digital signal that a computer can use using an Arduino Microcontroller. The Arduino has accompanying software to control the output of temperature readings, but in terms of the hardware, I've soldered all the wires and all the connections into place so all you need to do is make sure that the Arduino is wired into the computer using the USB power source when you are ready to run tests. I also designed the Arduino shield (the read board on top of the Arduino -- a nice alternative to breadboard circuitry) to fully support adding more thermocouples in the future.
![Arduino with Shield Connected to the Thermocouple](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/Hardware/Arduino.JPG)



### Software
Once all of the hardware is plugged in and running you can move on to initiating the software. The program I've written interfaces between all the hardware modules and allows for the user to quickly and easy start the data collection process. 

The first thing we need to do is ensure that the thermocouple is working and providing updated and accurate temperature readings. Open the folder [Thermocouple1](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/tree/master/thermocouple1) and download the file named "thermocouple1.ino". This is an Arduino program, so if you don't have Arduino (the program) installed, go to their website and download it. Once you have this program open and the Arduino connected through the USB cable, upload the script (little arrow pointing right at the top left). If all goes well you should not get any red error message and you can now see current temperature readings under tools>serial monitor.

With this done, we can now move on to the main automation program, but before do this again make sure that the LCR meter is turned on and connected to the computer.

In order to start collecting data, make sure that you have the program "LCRArduino.exe". Please note that this **must** be run on a Windows machine, and then when you download it Windows will try its best to convince you that the program is a virus and that you should not open it. Ignore this, force windows to open the program and enjoy your life virus free. 

You now have everything set up and are ready to begin collecting some data!

**QUICK CHECKLIST**
- Is the Arduino (little blue board with red board and colorful wires on top) plugged in with a blinking light? You need this working to get temperature data.
- Is the LCR meter plugged in and powered on? It is recommended to have it on for at least 1 hour before use.
- Is the sample placed in the sample holder correctly? Ensure no crossed wires or loose pellets.

## Automated LCR Program


Below is a detailed description of the "LCRArduino.exe" program.

- (Custom) LCR Meter Application window: 
	* Change Measurement Parameters - Click on any one of the parameters and use the drop down menu to change parameters
	* Track Negative Values - Click on any one of the parameters and use the checkbox next to each parameter to track its negative values
	* AC Information (Described in LCR meter manual) - To change the AC information click the Set button and change the values
	* DC Information (Described in LCR meter manual) - Currently preset and fixed - Will allow changes in future update
	* Single LCR Measurement - The green Trig button will run a single LCR Measurement
![Main Menu](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/DataCollection/Main.PNG)
- Automated window: (click on measure > Automated Measurement in main window)
	* Change Frequency/Temperature Sweep values - Click on Auto Setup then chose start/end values and number of points (if linear) or logarithmic (10).
	* Frequency Sweep Settings:   
	    + Interval Between Sweep Points - Time between each and every frequency measurement in sweep
		+ Number of Measurements - Repeat full frequency sweep N times for each temperature
		+ Interval Between Measurements - Time between each completed frequency sweep
	    + Average - Take an average of multiple frequency sweeps. Set in AC Information.
	* Temperature Sweep Settings 
	    + Delay at Sweep Temperature - Once you reach the specified target temperature, wait before starting frequency sweep
		 + Temperature Error Limit - Maximum allowed error between thermocouple read in temp and target temp
		(**NOTE:** Thermocouple reads in increments of 0.25C so anything change under 0.25C is rounded to 0)
	* Output Settings: 
	    + Save To - Chose location to save the output file(s). Must be a folder.
	    + Output Type - Single txt file or multiple txt file. (**NOTE:** It is HIGHLY recommended that you use the multiple files option, as you cannot run calculations or plot the the single text file option.You can only convert it to an excel spreadsheet).
	* Start: If all the parameters (a-d) are set and are within allowed limits the automation beings. After you click Start and no errors show up, START THE FURNACE
	* Standby Monitor: Clicking start will open the standby monitor, if you close it, you can re-open it by clicking this button.
![Automated Menu](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/DataCollection/Automation.PNG)
- Standby Window: (This window is meant only to display information, nothing can be changed here in regards to the automation procedure)
	* Sweep Values - All the LCR data recorder goes through here. From here it is placed in the corresponding text file.
	* Error/Negative Values - Based on the parameters you decided to track for negative values, corresponding  LCR data will show up here.
	* Current Temperature - The current temperature in the furnace chamber according to the thermocouple. (NOTE: Do not worry if the value here is different from the value on the furnace PID, this is expected)
	* Next Target - The next temperature target in the temperature sweep.
	* Close Window - Closes the standby window, returns to the Automated window.
	* Terminate Program - (BIG RED BUTTON - BEWARE!!) Force quits the entire program, regardless of what is going on in the background.
![Standby Menu](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/DataCollection/Standby.PNG)

Once you the automation process finishes, press the red Terminate Program button to close the program, unplug the USB connection to the Arduino and turn off the LCR meter (if you have finished taking data) 



# Data Analysis
The data you just took using the LCR meter should all be inside a folder and in text form. In order to make this data more friendly, we need to convert it into an excel spreadsheet. The three main products we will get out of this section are an excel spreadsheet of all the pure LCR data, another LCR sheet with all the electrical value calculations, and most importantly, full plots.

This program, `txt-xlsx.py` is entirely console based program (no graphics like the other one). This may seem a bit difficult at first but in reality it is quite simple. The two main functionalities of this program are Convert and Plot. Below are detailed descriptions of both.

## Analysis Setup
In order for this program to run on your computer, you need to make sure you have Python installed. Python is one of the biggest and more important programming languages in the world. If you are new to it I highly recommend you play around with it, as it is incredibly useful to research and data collection/processing.  If you need help installing it, watch [this video.](https://www.youtube.com/watch?v=OV9WlTd9a2U)


Once Python is up and running on your computer you need to install a few Python packages to run my program. If you are on windows open Command Line or Terminal if you are on MacOS, then enter the following commands one by one:
    `pip install numpy`
    `pip install xlrd`
    `pip install xlwt`
    `pip install xlsxwriter` 
    `pip install matplotlib` 
    
**NOTE:** If the program immediately crashes once you click on it, you likely missed a step, go though all the steps I listed very carefully before you begin converting and plotting your data, as once you finish doing it once you never need to do it again!
    
If everything works, you should click on the `txt-xlsx.py` program, where a black console should pop up with a single line looking something like this:

    Would you like to convert text data ("convert") or plot data from an excel sheet? ("plot")?

You are now ready to begin Analyzing your data!

## Text to Excel Conversion

The first thing you can do with the program is convert your raw LCR text data into excel sheets. Through this conversion step you end up with two excel spreadsheets, once with your plain data and one with all the electrical values calculated. Below are detailed steps as to how to get these spreadsheets:


1) Once you enter "convert" at the start of the program you will be asked to select a folder containing text file data. The folder you select must be the same one containing the data provided by the LCR-Furnace Program, containing the text files.
2) Once you select the folder, if the data is in the correct form, it will quickly be converted to an excel spreadsheet.
3) Your next option is if you want to run calculations on the data. If you wish to do so enter 'y', if not enter 'n' (without the quote marks). (**NOTE:** the data must be calculated (file should be called "_____.calcultions.xlsx") in order to plot it.) 
    * In order to run these calculations you must provide the pellet dimensions, all in millimeters, as well as the parameter list used when collecting data.
4) If all goes well, the program will notify you that the calculations were a success and prompt you to plot the data set that was just created. If you wish to plot enter 'y' and move on to Plot, if not enter 'n' (again without the quote marks) and the program should promptly close.

Below is an image of what the conversion process should look like:
![Convert Mode](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/DataConversion/convert.PNG)

## Plotting Your Data

1) If moving on from Conversion, the program will automatically cary on using the same data set, but if only starting now (using set made earlier) then the program will prompt you to choose a data set to plot. In order to plot, this data set MUST meet a certain set of criteria.
	- The original data set provided by the LCR-Furnace program should be given in multiple text files (multiple excel sheets), rather than just one.
	- The file you wish to plot must have calculations done on it already, and have the name _____.Calculations.xlsx (Note: Done automatically when completing the Conversion step).
2) If all goes well, the program should now prompt you to enter a list of temperatures and frequencies. These will be the values used in the corresponding plots legend, be it the frequency sweep or temperature sweep respectively. 
3) Next you will see a list of plotting options starting from -1. 
    - In order to create a plot enter two numbers representing the values you wish to plot in the order [xParameter,yParameter] replacing the xParameter/yParameter with your number of choice and not including the brackets.
	- Alternatively you can change the plot features by entering -1. This will open the  file `PlotParameters.py` which you can also edit just by  clicking on the filename This file is where you can edit how the plot looks, be very cautious when editing however, as if you mess up on a quote mark or spelling then the plot might not show up.
    - The final option is to quit by entering 'q'. This will end the program.
    
Below is an image of what the Plotting process should look like:
![Plot Mode](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/DataConversion/plotSuite.PNG)

And here is a sample plot, made using a Gallium Tungston Oxide (GWO) pellet, constructed using the Pythons Matplotlib plotting library.

![Sample Plot](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/blob/master/Images/DataConversion/plot1.png)

 
    
# Troubleshooting

Here are some potential problems you may run into as well as a few ways to deal with them:

1) When I click start on `LCRArduino.exe` I get an error telling me to recheck the parameters -- I did, everything looks right but the program refuses to start
    - Check that your frequency sweeps and temperature sweeps are within allowed ranges, 4Hz-8MHz and 0C-1000C respectively
    - Check that you selected a destination folder under the "Save To" section. 
2) After I click start on `LCRArduino.exe`, I get an error COM3 is not connected, after which the program crashes.
    - The LCR meter is not plugged in or not powered on. COM3 is the connection to the LCR, so make sure that it is functional and connected to the computer. See the [Hardware](#hardware) section for more details.
3) After I click start on `LCRArduino.exe`, I get an error COM4 is not connected, after which the program crashes.
    - The Arduino is not connected correctly or not powered on. COM4 is the connection to the Arduino, so make sure that it is functional and connected to the computer. 
    - If you connect it and still get the error, look at the Arduino (not the red mask on top, the blue board itself). 
        + If you **do not** see any blinking lights the board is likely fried and dead. The good news is that you can get a new one on amazon for cheap, remove the old mask with all the circuitry on top and continue as normal once you reload the program. See the [Hardware](#hardware) section for more details.
        + If you **do** see blinking lights then likely the program is not loaded correctly. See the [Software](#software) section under Arduino as to how you would reload the program.
        
4) When I open the `txt-xlsx.py` program it opens briefly but immediately closes.
    - Either Python or the supporting libraries I used to create Excel spreadsheets and plot are not installed on your computer. Go back to the [Setup](#analysis-setup) section and carefully follow the installation steps. 
5) When I try to plot an excel sheet on `txt-xlsx.py`, the program crashes.
    - Make sure that you are using an Excel spreadsheet that ends in ".calculations.xlsx". You **cannot** use the plain Excel spreadsheet to plot as it does not have the calculations. 
6) When I enter two numbers (x,y) on `txt-xlsx.py` to plot which are in the allowed range, the program still claims that something is wrong with my input parameters even though everything seems correct.
    - It is likely that if/when you changed the `plotParameters.py` file that you either misspelled something or forgot a single or double quote somewhere. Go back and try to find your mistake, or re-download the original file [here.](https://github.com/ronbroner/Dielectric-Analysis-and-Collection-System/tree/master/Convert-Plot)
    - If the program persists, check your temperature and frequency values and make sure that they actually exist in your data.
    
    
# Future Work

As you just saw, whole process is designed to be relatively simple and easy to use, so once you get the hang of it you're welcome to branch off and experiment with the code. I left some things unfinished, so if you'd like you can continue the project be my guest! Just we warned that there are a lot of moving parts under the hood, so be careful when changing things.

Here are some of the things that I left unfinished:
- Second Thermocouple board is attached but not working. If you can figure it out then your temperature readings should be more precise.
- Implement DC functionality to the LCRArduino program, allowing for more functionality
- Making the command line plotting software a GUI which would be easier to use. 
- Add Fitting and more extensive plotting tools to the plotting suite.

# Contact
I hope this procedure was easy enough to follow and yet deep enough to answer any question you may have. I added the Troubleshooting section to hopefully address any serious problems you may encounter, but if all else fails and you run into a huge problem or bug, please feel free to contact me at through email at ronbroner@ucsb.edu.


# Acknowledgements
Thank you to the NSF-PREM program for the funding and mentorship opportunities provided. 


Made By Ron Broner - Summer 2019

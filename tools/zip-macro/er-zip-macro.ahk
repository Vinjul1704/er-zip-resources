; VERSION DATE: 08.12.23

#NoEnv
#MaxHotkeysPerInterval 99000000
#HotkeyInterval 99000000
#KeyHistory 0
#SingleInstance Off
ListLines Off
Process, Priority, , A
SetBatchLines, -1
SetKeyDelay, -1, -1
SetMouseDelay, -1
SetDefaultMouseSpeed, 0
SetWinDelay, -1
SetControlDelay, -1
SendMode Input


Gui, -MinimizeBox

Gui, Font, norm bold
Gui, Add, GroupBox, x-1 y5 w500 h1000, Key Bindings
Gui, Font, norm

Gui, Add, Text, x10 y25 w50 h13, Hotkey
Gui, Add, Hotkey, x10 y40 w60 h21 vZipHotkey gHotkeyChanged

Gui, Add, Text, x80 y25 w70 h13, Move Key
Gui, Add, Hotkey, x80 y40 w40 h21 vMoveKey, w

Gui, Font, norm bold
Gui, Add, GroupBox, x-1 y80 w500 h1000, Primary Timings
Gui, Font, norm

Gui, Add, Text, x10 y100 w40 h13, Wait
Gui, Add, Edit, x10 y115 w45 h21 vPrimaryWait Number Limit3
Gui, Add, UpDown, Range0-999, 126

Gui, Add, Text, x65 y100 w40 h13, Move
Gui, Add, Edit, x65 y115 w35 h21 vPrimaryMove Number Limit2
Gui, Add, UpDown, Range0-99, 7

Gui, Add, Text, x110 y100 w30 h13, Cycle
Gui, Add, Edit, x110 y115 w35 h21 vPrimaryCycle Number Limit2
Gui, Add, UpDown, Range0-99, 0

Gui, Font, norm bold
Gui, Add, GroupBox, x-1 y155 w500 h1000, Extension Timings
Gui, Font, norm

Gui, Add, Text, x10 y175 w60 h13, Wait
Gui, Add, Edit, x10 y190 w35 h21 vExtensionWait Number Limit2
Gui, Add, UpDown, Range0-99, 0

Gui, Add, Text, x55 y175 w60 h13, Move
Gui, Add, Edit, x55 y190 w35 h21 vExtensionMove Number Limit2
Gui, Add, UpDown, Range0-99, 2

Gui, Font, norm bold
Gui, Add, GroupBox, x-1 y230 w500 h1000, Advanced Options
Gui, Font, norm

Gui, Add, Text, x10 y250 w75 h13, Timing Method
Gui, Add, DropDownList, x10 y265 w70 vTimingMethod Choose2, Accurate|Mixed|DLL|DLLv2|Sleep

Gui, Show, w155 h296, ER Zip Macro

return


HotkeyChanged:

	global ZipHotkey

	if (SavedZipHotkey) {
		Hotkey, %SavedZipHotkey%, DoZip, Off
	}

	Gui, Submit, NoHide

	Hotkey, % ZipHotkey, DoZip, On
	SavedZipHotkey = %ZipHotkey%

	return


DoZip:

	; GET GUI TIMING VALUES
	GuiControlGet, MoveKey

	GuiControlGet, PrimaryWait
	GuiControlGet, PrimaryMove
	
	GuiControlGet, PrimaryCycle
	
	GuiControlGet, ExtensionWait
	GuiControlGet, ExtensionMove
	
	GuiControlGet, TimingMethod
	
	
	; PRE-CALCULATE TIMINGS
	PrimaryWaitFinal := (PrimaryWait + 120 * PrimaryCycle) * 1000 / 60
	PrimaryMoveFinal := PrimaryMove * 1000 / 60
	
	ExtensionWaitFinal := ExtensionWait * 1000 / 60
	ExtensionMoveFinal := ExtensionMove * 1000 / 60
	
	ReleaseDelayFinal := 30 * 1000 / 60
	
	
	; RUN PRE-MACRO TIMER FUNCTION
	if IsFunc("TimerPre" TimingMethod)
		TimerPre%TimingMethod%()
	
	
	; STORE TIMER FUNCTION
	TimerFunc := Func("Timer" TimingMethod)
	
	
	; RUN MACRO
	if (ExtensionWait > 0) {

		Send, {RButton down}
		Send, {LAlt down}
		%TimerFunc%(PrimaryWaitFinal)
		Send, {%MoveKey% down}
		%TimerFunc%(PrimaryMoveFinal)
		Send, {%MoveKey% up}

		%TimerFunc%(ExtensionWaitFinal)
		Send, {%MoveKey% down}
		%TimerFunc%(ExtensionMoveFinal)
		Send, {%MoveKey% up}
		
		%TimerFunc%(ReleaseDelayFinal)
		Send, {LAlt Up}
		Send, {RButton up}

	} else {
	
		Send, {RButton down}
		Send, {LAlt down}
		%TimerFunc%(PrimaryWaitFinal)
		Send, {%MoveKey% down}
		%TimerFunc%(PrimaryMoveFinal)
		Send, {%MoveKey% up}
		
		%TimerFunc%(ReleaseDelayFinal)
		Send, {LAlt Up}
		Send, {RButton up}

	}
	
	
	; RUN POST-MACRO TIMER FUNCTION
	if IsFunc("TimerPost" TimingMethod)
		TimerPost%TimingMethod%()
		
		
	; MACRO DONE
	return


TimerAccurate(timeInMs) {

	DllCall("QueryPerformanceFrequency", "Int64*", freq)
	DllCall("QueryPerformanceCounter", "Int64*", CounterBefore)
	DllCall("QueryPerformanceCounter", "Int64*", CounterAfter)

	while (((CounterAfter - CounterBefore) / freq * 1000) < timeInMs) {
		DllCall("QueryPerformanceCounter", "Int64*", CounterAfter)
	}

	return
}


TimerMixed(timeInMs) {

	DllCall("QueryPerformanceFrequency", "Int64*", freq)
	DllCall("QueryPerformanceCounter", "Int64*", CounterBefore)
	DllCall("QueryPerformanceCounter", "Int64*", CounterAfter)

	preSleep := timeInMs - 20

	if (preSleep > 0) {
		DllCall("Sleep", "UInt", preSleep)
	}

	while (((CounterAfter - CounterBefore) / freq * 1000) < timeInMs) {
		DllCall("QueryPerformanceCounter", "Int64*", CounterAfter)
	}
	
	return
}


TimerDLL(timeInMs) {

	DllCall("Sleep", UInt, timeInMs)

	return
}


TimerDLLv2(timeInMs) {
	
	DllCall("Sleep", UInt, timeInMs)
	
	return
}

TimerPreDLLv2() {

	DllCall("ntdll\ZwSetTimerResolution", "Int", 10000, "Int", 1, "Int*", CurTimerResolution)
	
	return
}

TimerPostDLLv2() {

	DllCall("ntdll\ZwSetTimerResolution", "Int", 10000, "Int", 0, "Int*", CurTimerResolution)
	
	return
}


TimerSleep(timeInMs) {

	Sleep timeInMs

	return
}


GuiClose: 
	ExitApp

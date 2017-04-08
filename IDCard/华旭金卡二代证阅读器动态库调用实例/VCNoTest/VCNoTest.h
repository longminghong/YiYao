// VCNoTest.h : main header file for the VCNOTEST application
//

#if !defined(AFX_VCNOTEST_H__6209CC6A_5C06_4567_B4FA_358F109B01AF__INCLUDED_)
#define AFX_VCNOTEST_H__6209CC6A_5C06_4567_B4FA_358F109B01AF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CVCNoTestApp:
// See VCNoTest.cpp for the implementation of this class
//

class CVCNoTestApp : public CWinApp
{
public:
	CVCNoTestApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CVCNoTestApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CVCNoTestApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_VCNOTEST_H__6209CC6A_5C06_4567_B4FA_358F109B01AF__INCLUDED_)

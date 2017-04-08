// VCNoTestDlg.h : header file
//

#if !defined(AFX_VCNOTESTDLG_H__12570659_8310_418B_9D1D_C3784A5E6BB1__INCLUDED_)
#define AFX_VCNOTESTDLG_H__12570659_8310_418B_9D1D_C3784A5E6BB1__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CVCNoTestDlg dialog

class CVCNoTestDlg : public CDialog
{
// Construction
public:
	CVCNoTestDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CVCNoTestDlg)
	enum { IDD = IDD_VCNOTEST_DIALOG };
	CString	m_Name;
	CString	m_Sex;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CVCNoTestDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CVCNoTestDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnButton1();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_VCNOTESTDLG_H__12570659_8310_418B_9D1D_C3784A5E6BB1__INCLUDED_)

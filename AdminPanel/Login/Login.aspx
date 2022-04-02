<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Content_AdminPanel_Login_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title>Login</title>
	<link rel="icon" type="image/png" href="images/icons/favicon.ico"/>
	<link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css">
	<link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css">
	<link rel="stylesheet" type="text/css" href="fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
	<link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">
	<link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">
	<link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css">
	<link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">
	<link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css">
	<link rel="stylesheet" type="text/css" href="css/util.css">
	<link rel="stylesheet" type="text/css" href="css/main.css">
</head>
<body>
    <form id="form1" runat="server" CssClass="login100-form validate-form p-b-33 p-t-5">
    <div>

	
	<div class="limiter">
		<div class="container-login100" style="background-image: url('images/bg-02.jpg');">
			<div class="wrap-login100 p-t-30 p-b-50">
				<span class="login100-form-title p-b-41">
					Account Login
				</span>
				
                    <asp:Label runat="server" ID="lblMessage" CssClass="text-warning"></asp:Label>
					<div class="wrap-input100 validate-input" data-validate = "Enter username"><span style="color:red">*</span>
						<asp:TextBox class="input100" runat="server" id="txtUserName" placeholder="User name"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xe82a;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Enter password"><span style="color:red">*</span>
						<asp:TextBox class="input100" type="password" id="txtPassword" runat="server" placeholder="Password"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xe80f;"></span>
					</div>

					<div class="container-login100-form-btn m-t-32">
						<asp:Button runat="server" ID="btnLogin" Text="Login" class="login100-form-btn" OnClick="btnLogin_Click"/>
					</div>
                    <div class="container-login100-form-btn m-t-32">
						<asp:Button runat="server" ID="btnRegistor" CssClass=" btn-link" Text="New Here ! Registor" class="login100-form-btn" OnClick="btnRegistor_Click" />
					</div>

                
			</div>
                 <div>
                    <asp:HyperLink ID="hlRegistor" runat="server" CssClass="btn btn-lg" NavigateUrl="~/AdminPanel/Registor/Registor"></asp:HyperLink>
                </div>
		</div>
	</div>
	

	<div id="dropDownSelect1"></div>
	
	<script src="vendor/jquery/jquery-3.2.1.min.js"></script>
	<script src="vendor/animsition/js/animsition.min.js"></script>
	<script src="vendor/bootstrap/js/popper.js"></script>
	<script src="vendor/bootstrap/js/bootstrap.min.js"></script>
	<script src="vendor/select2/select2.min.js"></script>
	<script src="vendor/daterangepicker/moment.min.js"></script>
	<script src="vendor/daterangepicker/daterangepicker.js"></script>
	<script src="vendor/countdowntime/countdowntime.js"></script>
	<script src="js/main.js"></script>
    </div>
    </form>
</body>
</html>

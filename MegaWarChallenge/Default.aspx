<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MegaWarChallenge.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/Site.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

    <div id="GameArea" class="container">
    <div id="Player2Area" class="row fixed-height">
    
        <div class="columnEdge"><asp:Image ID="player2deckImage" runat="server"/></div>
        <div id="player2Info" class="columnCenter"><h1>Player 2 Info</h1></div>
        <div id="spaceHolderRight" class="columnEdge">spaceholderright</div>
    
    </div>

    <div id="battleground" class ="row">
    <asp:Label ID="unshuffledLabel" runat="server"></asp:Label>
    </div>

    <div id="Player1Area" class ="row fixed-height">
        <div id="spaceHolderLeft" class ="columnEdge">spaceholderleft</div>
        <div id="player1Info" class ="columnCenter"><h1>Player 1 Info</h1></div>
        <div class="columnEdge"><asp:Image ID="player1deckImage" runat="server" /></div>

    </div>

        
    </div>

    </form>
</body>
</html>

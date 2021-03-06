﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="MegaWarChallenge.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <link rel="stylesheet" href="Content/Site.css" />
    <title>War</title>

	
    <script type="text/javascript" src="//secure.gbcblue.com/common/jquery/jquery-1.11.1.min.js"></script> 
    <script type="text/javascript" src="//secure.gbcblue.com/common/jquery.event.swipe/jquery.event.swipe.0.5.2.js"></script>
    <script type="text/javascript" src="//secure.gbcblue.com/common/fancybox/2.1.5/source/jquery.fancybox.js"></script>
    <script type="text/javascript" src="//secure.gbcblue.com/common/fancybox/2.1.5/source/jquery.fancybox.swipe.js"></script>
    <link rel="stylesheet" type="text/css" href="//secure.gbcblue.com/common/fancybox/2.1.5/source/jquery.fancybox.css" />
    
    

</head>

    <script type="text/javascript">

        $(document).ready(function () {
            $(".fancyboxLink").fancybox({
                autoSize: true,
                fitToView: true,
                maxWidth: 400,
                topRatio: 0.1,
                closeBtn: true,
                closeClick: true,

            })

            //works for initial window size but is not dynamic...(or is it?)
            $(".tableCard").width($('#player2deckImage').width())
            //so let's also put it in the resize listener
            $(window).resize(function () { $(".tableCard").width($('#player2deckImage').width()) });
            
        })
        
        function Trigger() {
            $("#welcomePopUplink").trigger("click");
        }

       
    </script>
<body>
    <form id="form1" runat="server">
        <a id="welcomePopUplink" class="fancyboxLink" href="#welcomePopUp" runat="server"></a>

    <div id="GameArea" class="container">
        <div id="welcomePopUp" style="display:none;">
            <div id="welcomPopUpText">Let's Play a Game of War</div>
        </div>
    <div id="Player2Area" class="row fixed-height">
    
        <div class="columnEdge"><asp:Image ID="player2deckImage" runat="server"/></div>
        <div id="player2Info" class="columnCenter">
            <h1>Player 2 Info</h1>
            <asp:Label ID="p2cardCountLabel" CssClass="cardCount" runat="server">Card count: </asp:Label>

        </div>
        <div id="spaceHolderRight" class="columnEdge">&nbsp;</div>
    
    </div>

    <div id="battleground" class ="row" style="text-align:center">&nbsp;
        <div id="leftInfoPane">&nbsp;
            <asp:Label ID="unshuffledLabel" runat="server" CssClass="elbow-room"></asp:Label>
            <asp:Label ID="resultsLabel" runat="server" CssClass="elbow-room"></asp:Label>

        </div>
    
       
        <%--<asp:Button ID="warButton" runat="server" Text="Auto-Play" CssClass="elbow-room" OnClick="warButton_Click" />--%>
        
        
        <div id="cardArea">
        <asp:Image ID="p2LeftCard" runat="server" CssClass="tableCard" />
        <asp:Image ID="p2MainCard" runat="server" CssClass="tableCard" />
        <asp:Image ID="p2RightCard" runat="server" CssClass="tableCard"/>
        <br />
        <asp:Image ID="p1LeftCard" runat="server" CssClass="tableCard"/>
        <asp:Image ID="p1MainCard" runat="server" CssClass="tableCard"/>
        <asp:Image ID="p1RightCard" runat="server" CssClass="tableCard"/>
            </div>

        <div id="rightInfoPane" runat="server">&nbsp;</div>
    </div>

    <div id="Player1Area" class ="row fixed-height">
        <div id="spaceHolderLeft" class ="columnEdge">&nbsp;</div>
        <div id="player1Info" class ="columnCenter">
            <h1>Player 1 Info</h1>
            <asp:Label ID="p1cardCountLabel" CssClass="cardCount" runat="server">Card count: </asp:Label><br />
             <asp:Button ID="throwCardButton" runat="server" OnClick="throwCardButton_Click" Text="Throw Down!" CssClass="elbow-room button"/>
        </div>
        <div class="columnEdge"><asp:Image ID="player1deckImage" runat="server" /></div>

    </div>

        
    </div>

    </form>
</body>
</html>

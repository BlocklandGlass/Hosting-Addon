if(!isObject(GlassHostingButton)) {
  new GuiBitmapButtonCtrl(GlassHostingButton) {
     profile = "GlassRoundedButtonProfile";
     horizSizing = "right";
     vertSizing = "top";
     position = "6 490";
     extent = "109 30";
     minExtent = "8 2";
     enabled = "1";
     visible = "1";
     clipToParent = "1";
     command = "GlassHosting::toggleWindow();";
     groupNum = "-1";
     buttonType = "PushButton";
     bitmap = "Add-Ons/Client_GlassHosting/img/btn_hosting";
     lockAspectRatio = "0";
     alignLeft = "0";
     alignTop = "0";
     overflowImage = "0";
     mKeepCached = "0";
     mColor = "255 255 255 255";

     text = "";
  };
}

function GlassHostingGui::buildPanel(%instance) {
  %instance = %instance.getId();
  %id = (GlassHostingGui_Window.panels++)-1;
  %instance.guiId = %id;

  %tab = new GuiSwatchCtrl("GlassHostingGui_PanelTab" @ %id) {
     profile = "GuiDefaultProfile";
     horizSizing = "right";
     vertSizing = "bottom";
     position = "10" SPC (%id*30)+65;
     extent = "65 25";
     minExtent = "8 2";
     enabled = "1";
     visible = "1";
     clipToParent = "1";
     color = "220 220 220 255";
     instance = %instance;

     new GuiTextCtrl() {
        profile = "GlassFriendTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "7 3";
        extent = "49 17";
        minExtent = "8 2";
        enabled = "1";
        visible = "1";
        clipToParent = "1";
        text = %instance.ident;
        maxLength = "255";
     };

     new GuiMouseEventCtrl(GlassHostingGui_PanelTabMouse) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "70 25";
        instance = %instance;
     };
  };

  %panel = new GuiSwatchCtrl("GlassHostingGui_Panel" @ %id) {
    profile = "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "80 35";
    extent = "680 530";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    color = "220 220 220 255";
    instance = %instance;
  };

  %panel.headerSwatch = new GuiSwatchCtrl() {
    profile = "GuiDefaultProfile";
    horizSizing = "center";
    vertSizing = "bottom";
    position = "10 10";
    extent = "660 80";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    color = "255 255 255 255";
  };

  %panel.button1 = new GuiBitmapButtonCtrl() {
    profile = "GlassBlockButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 45";
    extent = "100 25";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    command = "GlassHostingGui::panelButton1(" @ %instance @ ");";
    text = "Start Server";
    groupNum = "-1";
    buttonType = "PushButton";
    bitmap = "~/System_BlocklandGlass/image/gui/btn";
    lockAspectRatio = "0";
    alignLeft = "0";
    alignTop = "0";
    overflowImage = "0";
    mKeepCached = "0";
    mColor = "220 220 220 255";
  };

  %panel.button2 = new GuiBitmapButtonCtrl() {
    profile = "GlassBlockButtonWhiteProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "120 45";
    extent = "100 25";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    command = "GlassHostingGui::panelButton2(" @ %instance @ ");";
    text = "Kill Server";
    groupNum = "-1";
    buttonType = "PushButton";
    bitmap = "~/System_BlocklandGlass/image/gui/btn";
    lockAspectRatio = "0";
    alignLeft = "0";
    alignTop = "0";
    overflowImage = "0";
    mKeepCached = "0";
    mColor = "231 76 60 255";
  };

  %panel.header = new GuiMLTextCtrl() {
    profile = "GuiMLTextProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 10";
    extent = "640 30";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    lineSpacing = "2";
    allowColorChars = "0";
    maxChars = "-1";
    text = "<font:verdana bold:16>Loading...<br><font:verdana:14><color:ff0000>Connecting...";
    maxBitmapHeight = "-1";
    selectable = "1";
    autoResize = "1";
  };

  %panel.scrollContainer = new GuiSwatchCtrl() {
    profile = "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 130";
    extent = "660 390";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    color = "255 255 255 255";
  };

  %panel.scroll = new GuiScrollCtrl() {
    profile = "GlassScrollProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 10";
    extent = "640 348";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    willFirstRespond = "0";
    hScrollBar = "alwaysOff";
    vScrollBar = "alwaysOn";
    constantThumbHeight = "0";
    childMargin = "0 0";
    rowHeight = "40";
    columnWidth = "30";
  };

  %panel.scrollSwatch = new GuiSwatchCtrl() {
    profile = "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "630 344";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    color = "255 255 255 0";
  };

  %panel.scrollText = new GuiMLTextCtrl() {
    profile = "GuiConsoleProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "5 5";
    extent = "620 10";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    lineSpacing = "2";
    allowColorChars = "1";
    maxChars = "-1";
    text = "";
    maxBitmapHeight = "-1";
    selectable = "1";
    autoResize = "1";
  };

  %panel.input = new GuiTextEditCtrl() {
    profile = "GlassTextEditProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 364";
    extent = "640 16";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    maxLength = "255";
    historySize = "0";
    password = "0";
    tabComplete = "0";
    sinkAllKeyEvents = "0";
    altcommand = "GlassHostingGui::panelInput(" @ %instance @ ");";
    accelerator = "enter";
  };

  %panel.tab1 = new GuiBitmapButtonCtrl() {
    profile = "GlassBlockButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 100";
    extent = "330 30";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    text = "Console";
    groupNum = "-1";
    buttonType = "PushButton";
    bitmap = "~/System_BlocklandGlass/image/gui/tab1";
    lockAspectRatio = "0";
    alignLeft = "0";
    alignTop = "0";
    overflowImage = "0";
    mKeepCached = "0";
    mColor = "220 220 220 200";
    command = "GlassHostingGui::panelTab1(" @ %instance @ ");";
  };

  %panel.tab2 = new GuiBitmapButtonCtrl() {
    profile = "GlassBlockButtonProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "340 100";
    extent = "330 30";
    minExtent = "8 2";
    enabled = "1";
    visible = "1";
    clipToParent = "1";
    text = "Chat";
    groupNum = "-1";
    buttonType = "PushButton";
    bitmap = "~/System_BlocklandGlass/image/gui/tab1";
    lockAspectRatio = "0";
    alignLeft = "0";
    alignTop = "0";
    overflowImage = "0";
    mKeepCached = "0";
    mColor = "131 195 243 200";
    command = "GlassHostingGui::panelTab2(" @ %instance @ ");";
  };

  %panel.add(%panel.headerSwatch);
  %panel.headerSwatch.add(%panel.header);
  %panel.headerSwatch.add(%panel.button1);
  %panel.headerSwatch.add(%panel.button1);
  %panel.add(%panel.tab1);
  %panel.add(%panel.tab2);
  %panel.add(%panel.scrollContainer);
  %panel.scrollContainer.add(%panel.input);
  %panel.scrollContainer.add(%panel.scroll);
  %panel.scroll.add(%panel.scrollSwatch);
  %panel.scrollSwatch.add(%panel.scrollText);

  GlassHostingGui_Window.add(%panel);
  GlassHostingGui_Window.add(%tab);

  %instance.panel = %panel;
  %instance.tab   = %tab;

  if(!isObject(GlassHostingGui_Panels)) {
    new SimSet(GlassHostingGui_Panels);
  }

  GlassHostingGui_Panels.add(%panel);

  if(!isObject(GlassHostingGui_Tabs)) {
    new SimSet(GlassHostingGui_Tabs);
  }

  GlassHostingGui_Tabs.add(%tab);

  GlassHostingGui::openTab(%id);

  return %panel;
}

function GlassHostingGui::openTab(%id) {
  $GlassHosting::OpenTab = %id;
  for(%i = 0; %i < GlassHostingGui_Tabs.getCount(); %i++) {
    GlassHostingGui_Tabs.getObject(%i).extent = "65 25";
  }

  for(%i = 0; %i < GlassHostingGui_Panels.getCount(); %i++) {
    GlassHostingGui_Panels.getObject(%i).setVisible(false);
    GlassHostingGui_Panels.getObject(%i).instance.panelOpen = false;
  }

  GlassHostingGui_AddPanel.setVisible(false);
  GlassHostingGui_AddTab.extent = "65 25";

  if(%id $= "add" || %id $= "") {
    %panel = GlassHostingGui_AddPanel;
    %tab = GlassHostingGui_AddTab;
  } else {
    %panel = "GlassHostingGui_Panel" @ %id;
    %tab = "GlassHostingGui_PanelTab" @ %id;
  }

  %panel.setVisible(true);
  %tab.extent = "70 25";

  if(isObject(%panel.instance))
    %panel.instance.panelOpen = true;
}

function GlassHostingGui::updateHeader(%instance) {
  if(%instance.status $= "blockland.running") {
    %title = %instance.value["hostName"] @ "'s " @ %instance.value["serverName"];
  } else {
    %title = %instance.ident;
  }

  %statusText = getField($GlassHosting::Status[%instance.status], 0);
  %statusColor = getField($GlassHosting::Status[%instance.status], 1);

  %text = "<font:verdana bold:16>" @ %title @ "<br>";
  %text = %text @ "<font:verdana:14><color:" @ %statusColor @ ">" @ %statusText;

  %instance.panel.header.setText(%text);
}

function GlassHostingGui::updateButtons(%instance) {
  %canStart = getField($GlassHosting::Status[%instance.status], 2);
  %canStop = getField($GlassHosting::Status[%instance.status],  3);
  %canKill = getField($GlassHosting::Status[%instance.status],  4);

  %button1 = %instance.panel.button1;
  %button2 = %instance.panel.button2;

  %button2.setVisible(false);

  if(%canStart) {
    %button1.setText("Start Server");
    %button1.setVisible(true);
  } else if(%canStop) {
    %button1.setText("Stop Server");
    %button1.setVisible(true);

    if(%canKill) {
      %button2.setText("Kill Server");
      %button2.setVisible(true);
    }
  } else {
    %button1.setVisible(false);
  }
}

function GlassHostingGui::pushLine(%instance, %line) {
  %panel = %instance.panel;

  //%line = strReplace(%line, "\n", "<br>");
  %panel.scrollText.setValue(%panel.scrollText.getValue() @ "\n" @ %line);

  if(%panel.scrollText.isAwake())
    %panel.scrollText.forceReflow();

  %panel.scrollSwatch.verticalMatchChildren(0, 2);
  %panel.scrollSwatch.setVisible(true);

  //%lp = %chatroom.getLowestPoint() - %chatroom.scroll.getLowestPoint();

  //if(%lp >= -50) {
    %panel.scroll.scrollToBottom();
  //}
}

function GlassHostingGui::setScrollContent(%instance, %text) {
  %panel = %instance.panel;

  %text = strReplace(%text, "\n", "<br>");
  %panel.scrollText.extent = "620 10";
  %panel.scrollText.setValue(%text);

  if(%panel.scrollText.isAwake())
    %panel.scrollText.forceReflow();

  %panel.scrollSwatch.verticalMatchChildren(0, 2);
  %panel.scrollSwatch.setVisible(true);

  %panel.scroll.scrollToBottom();
}

function GlassHostingGui::panelButton1(%instance) {
  if(%instance.status $= "blockland.running") {
    %instance.tcp.emit("stopServer");
  } else if(%instance.status $= "blockland.crashed" || %instance.status $= "idle") {
    %instance.tcp.emit("startServer");
  } else {
    messageBoxOk("I don't know what to do?", %instance.status);
  }
}

function GlassHostingGui::panelButton2(%instance) {
  %instance.tcp.emit("killServer");
}

function GlassHostingGui::panelInput(%instance) {
  if(%instance.status !$= "blockland.running" && %instance.status !$= "blockland.launch")
    return messageBoxOk("Not Ready!", "The server isn't running yet!");

  %input = %instance.panel.input.getValue();
  %instance.panel.input.setValue("");

  %evalObj = jettisonObject();

  if(%instance.consoleOpen) {

    if(trim(%input) $= "cls();") {
      GlassHostingGui::setScrollContent(%instance, "");
      %instance.console = "";
      %evalObj.delete();
      return;
    }

    %evalObj.set("eval", "string", %input);

  } else if(%instance.chatOpen) {

    %evalObj.set("eval", "string", "talk(\"" @ expandEscape(%input) @ "\");");

  }
  %instance.tcp.emit("eval", %evalObj);
  %evalObj.delete();
}

function GlassHostingGui::panelTab1(%instance) {
  %instance.loadConsole();
  %instance.panel.tab1.mColor = "131 195 243 200";
  %instance.panel.tab2.mColor = "220 220 220 200";
}

function GlassHostingGui::panelTab2(%instance) {
  %instance.loadChat();
  %instance.panel.tab2.mColor = "131 195 243 200";
  %instance.panel.tab1.mColor = "220 220 220 200";
}

function GlassHostingGui_PanelTabMouse::onMouseEnter(%this) {
  %this.down = false;

  %swatch = %this.getGroup();
  %instance = %this.instance;

  %swatch.color = "131 195 243 150";
  //%swatch.extent = "70 25";
}

function GlassHostingGui_PanelTabMouse::onMouseLeave(%this) {
  %this.down = false;

  %swatch = %this.getGroup();
  %instance = %this.instance;

  %swatch.color = "220 220 220 255";

  if(%instance)
    %id = %instance.guiId;

  if($GlassHosting::OpenTab !$= %id) {
    %swatch.extent = "65 25";
  }
}

function GlassHostingGui_PanelTabMouse::onMouseDown(%this) {
  %this.down = true;
}

function GlassHostingGui_PanelTabMouse::onMouseUp(%this) {
  if(%this.down) {
    if(isObject(%this.instance))
      %id = %this.instance.guiId;

    GlassHostingGui::openTab(%id);
  }
}

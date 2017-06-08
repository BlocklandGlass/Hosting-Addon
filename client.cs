if(!isObject(GlassHostingCodeProfile)) new GuiControlProfile(GlassHostingCodeProfile : GuiTextEditProfile) {
  fontType = "Verdana";
  fontSize = "25";
  fontColors[0] = "64 64 64 255";
  fontColors[1] = "128 128 128 255";

  fillColor = "240 240 240 255";
  borderColor = "128 128 128 255";

  justify = "center";
};

exec("./comm.cs");
exec("./gui.cs");

if(!isObject(GlassHostingGui))
  exec("./GlassHostingGui.gui");

function GlassHosting::registerStatus(%ident, %text, %color, %start, %stop, %kill) {
  switch$(%color) {
    case "red"   : %color = "e74c3c";
    case "yellow": %color = "f1c40f";
    case "green" : %color = "2ecc71";
  }

  $GlassHosting::Status[%ident] = %text TAB %color TAB %start TAB %stop TAB %kill;
}

if(!$GlassHosting::Status) {
  GlassHosting::registerStatus("idle", "Server Offline", "red", true, false, false);

  GlassHosting::registerStatus("blockland.running",   "Running", "green", false, true, false);
  GlassHosting::registerStatus("blockland.start",     "Starting Server", "yellow", false, false, true);
  GlassHosting::registerStatus("blockland.launch",    "Launching Server", "yellow", false, false, true);
  GlassHosting::registerStatus("blockland.quitting",  "Server Ending", "yellow", false, false, true);
  GlassHosting::registerStatus("blockland.crashed",   "Server Crashed", "red", true, false, false);

  GlassHosting::registerStatus("blockland.install",   "Installing Blockland", "yellow", false, false, false);
  GlassHosting::registerStatus("blockland.enter_key", "Blockland Key Needed!", "yellow", false, false, true);
  $GlassHosting::Status = true;
}

function GlassHosting::loadPanel(%ident, %address, %port) {
  echo("Loading " @ %ident);
  %instance = new ScriptObject(GlassHostingInstance) {
    ident = %ident;
    address = %address;
    port = %port;
  };

  GlassHostingGui::buildPanel(%instance);
  GlassHosting::connectToServer(%instance);
}

function GlassHostingInstance::onValueUpdate(%this, %key, %value) {
  if(%key $= "hostName" || %key $= "serverName") {
    GlassHostingGui::updateHeader(%this);
  }
}

function GlassHostingInstance::setStatus(%this, %status) {
  GlassHostingGui::updateHeader(%this);
  GlassHostingGui::updateButtons(%this);
}

function GlassHostingInstance::pushConsole(%this, %line) {
  //%i = %this.consoleLines+0;
  //%this.consoleLines++;
  //%this.consoleLine[%i] = %line;

  if(%this.console $= "")
    %this.console = %line;
  else
    %this.console = %this.console NL %line;

  if(%this.consoleOpen) {
    GlassHostingGui::pushLine(%this, %line);
  }
}

function GlassHostingInstance::loadConsole(%this) {
  %this.panel.scrollText.setProfile(GuiConsoleProfile);
  GlassHostingGui::setScrollContent(%this, %this.console);
  %this.chatOpen = false;
  %this.consoleOpen = true;
}

function GlassHostingInstance::pushChat(%this, %line) {
  if(%this.chat $= "")
    %this.chat = %line;
  else
    %this.chat = %this.chat NL %line;

  if(%this.chatOpen) {
    GlassHostingGui::pushLine(%this, %line);
  }
}

function GlassHostingInstance::loadChat(%this) {
  %this.panel.scrollText.setProfile(BlockChatTextSize4Profile);
  GlassHostingGui::setScrollContent(%this, %this.chat);
  %this.chatOpen = true;
  %this.consoleOpen = false;
}

function GlassHosting_pasteLoop() {
  cancel($GlassHosting::PasteSch);

  if(strlen(GlassHostingGui_Add1.getValue()) > 4)
    GlassHostingGui::codeInput(1);

  $GlassHosting::PasteSch = schedule(33, 0, GlassHosting_pasteLoop);
}

function GlassHostingGui::onWake(%this) {
  $GlassHosting::PasteSch = schedule(33, 0, GlassHosting_pasteLoop);
}

function GlassHostingGui::onSleep(%this) {
  cancel($GlassHosting::PasteSch);
}

function GlassHostingGui::codeInput(%id) {
  %obj = "GlassHostingGui_Add" @ %id;
  %val = %obj.getValue();
  %val = strReplace(%val, "-", "");

  %raw = %val;
  %val = "";

  %allowed = "abcdefghijklmnopqrstuvwxyz1234567890";
  %allowed = strupr(%allowed);

  for(%i = 0; %i < strlen(%raw); %i++) {
    %char = getSubStr(%raw, %i, 1);
    %char = strupr(%char);

    if(strpos(%allowed, %char) >= 0) {
      %val = %val @ %char;
    }
  }

  if(strlen(%val) >= 4) {
    %obj.setValue(getSubStr(%val, 0, 4));
    if(%id < 3) {
      %nextObj = "GlassHostingGui_Add" @ (%id+1);
      %nextObj.makeFirstResponder(true);

      if(%nextObj.getValue() $= "") {
        %nextObj.setValue(getSubStr(%val, 4, strlen(%val)));
        GlassHostingGui::codeInput(%id+1);
      }

    }
  }
}

function GlassHostingGui::codeSubmit() {
  %key = GlassHostingGui_Add1.getValue() @ GlassHostingGui_Add2.getValue() @ GlassHostingGui_Add3.getValue();
  GlassHosting::tryKey(%key, true);
}

function GlassHosting::tryKey(%key, %isNew) {
  %url          = "http://host.blocklandglass.com/api/client/new.php?code=" @ %key @ "&blid=" @ getNumKeyId() @ "&name=" @ urlenc($Pref::Player::NetName);
  %method       = "GET";
  %downloadPath = "";
  %className    = "GlassHostingAddTCP";
  %tcp = connectToURL(%url, %method, %downloadPath, %className);

  %tcp.key = %key;
  %tcp.isNew = %isNew;
}

function GlassHostingAddTCP::handleText(%this, %text) {
  %this.buffer = %this.buffer NL %text;
}

function GlassHostingAddTCP::onDone(%this, %error) {
  if(%error) {
    messageBoxOk("Error!", "There was an error contacting the Glass Hosting authentication system. Please try again later, and if the issue persists, open a support ticket.<br><br>Error Code: 1");
    return;
  }

  if(jettisonParse(%this.buffer)) {
    messageBoxOk("Error!", "There was an error contacting the Glass Hosting authentication system. Please try again later, and if the issue persists, open a support ticket.<br><br>Error Code: 2");
    return;
  }

  %obj = $JSON::Value;

  if(%obj.status $= "success") {
    %name = %obj.name;
    %address = %obj.address;

    %ip   = getSubStr(%address, 0, strpos(%address, ":"));
    %port = getSubStr(%address, strpos(%address, ":")+1, strlen(%address));

    if(%this.isNew) {
      $GlassHosting::Key[$GlassHosting::Keys+0] = %this.key;
      $GlassHosting::Keys++;
      export("$GlassHosting::Key*", "config/client/Glass/hostingPrefs.cs");
    }

    GlassHosting::loadPanel(%name, %ip, %port);
  } else {
    if(%this.isNew)
      messageBoxOk("Failed", "Failed to add server: " @ %obj.status);
    else
      echo("Failed to load Glass Hosting Server: " @ %obj.status);
  }
}

function GlassHosting::toggleWindow() {
  if(GlassHostingGui_Window.getGroup().getName() $= "GlassHostingGui") {
    GlassOverlayGui.add(GlassHostingGui_Window);
  }
  else if(GlassHostingGui_Window.visible)
    GlassHostingGui_Window.setVisible(false);
  else
    GlassHostingGui_Window.setVisible(true);
}

function GlassHosting::loadPanels() {
  if($GlassHosting::LoadedKeys) return;

  %file = "config/client/Glass/hostingPrefs.cs";
  if(!isFile(%file)) return;

  exec(%file);
  for(%i = 0; %i < $GlassHosting::Keys; %i++) {
    %key = $GlassHosting::Key[%i];
    if(%loaded[%key]) continue;

    GlassHosting::tryKey(%key, false);
    %loaded[%key] = true;
  }
  $GlassHosting::LoadedKeys = true;
}

GlassHosting::loadPanels();

package GlassHostingPackage {
  function MM_AuthBar::blinkSuccess(%this) {
    GlassHosting::loadPanels();
    parent::blinkSuccess(%this);
  }

  function GlassOverlay::open(%a) {
    if(GlassLiveModeratorButton.visible) {
      GlassHostingButton.position = vectorSub(GlassLiveModeratorButton.position, "0 40");
    } else {
      GlassHostingButton.position = GlassLiveModeratorButton.position;
    }

    GlassOverlay.add(GlassHostingButton);
    parent::open(%a);
  }
};
activatePackage(GlassHostingPackage);

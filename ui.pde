void draw() {
  background(0);
  while (console.size() >= 25) {
    console.remove(0);
  }
  fill(255);
  textAlign(LEFT, TOP);
  if (settings) {
    textSize(50);
    text("Settings", 0, 0);
      fill(150);
      rectMode(CORNER);
      rect(width - 130, height - 55, 125, 50);
      fill(255);
      textAlign(CENTER, CENTER);
      textSize(30);
      text("Back", width - 65, height - 30);
  } else {
    textSize(10);
    text(join(alta(console), '\n'), 0, 0);
    textSize(30);
    if (processing) {
      rectMode(CENTER);
      fill(255);
      rect(width / 2, height - 30, width - 50, 25);
      fill(0, 255, 0);
      rectMode(CORNER);
      rect(5, height - 42.5, (progress / float(amt)) * (width - 50), 25);
    } else {
      dropArrow(width / 2.0, height / 2.0 + sin(frameCount / 15.0) * 15);
      textAlign(CENTER, TOP);
      text("Drop a folder or texture\nto convert it!", width / 2.0, height / 2.0 + 30);
      fill(150);
      rectMode(CORNER);
      rect(width - 130, height - 55, 125, 50);
      fill(255);
      textAlign(CENTER, CENTER);
      textSize(30);
      text("Settings", width - 65, height - 30);
    }
    textAlign(RIGHT, TOP);
    textSize(13);
    fill(255);
    text("Credits\nVTFCmd: Neil Jedrzejewski & Ryan Gregg\nVTEX / VPK: Valve Corporation", width, 0);
  }
}

void mousePressed() {
  if (mouseRegion(width - 130, height - 55, width - 5, height - 5)) {
    settings = !settings;
  }
}

boolean mouseRegion(float x1, float y1, float x2, float y2) {
  return mouseX > x1 && mouseY > y1 && mouseX < x2 && mouseY < y2;
}

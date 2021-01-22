void draw() {
  background(0);
  while (console.size() >= 25) {
    console.remove(0);
  }
  if (settings) {
    
  } else {
    fill(255);
    stroke(255);
    textAlign(LEFT, TOP);
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
    }
    textAlign(RIGHT, TOP);
    textSize(13);
    fill(255);
    text("Credits\nVTFCmd: Neil Jedrzejewski & Ryan Gregg\nVTEX / VPK: Valve Corporation", width, 0);
  }
}

void mousePressed() {
  println(mouseRegion(0, 0, width/2, height/2));
}

boolean mouseRegion(float x1, float y1, float x2, float y2) {
  return mouseX > x1 && mouseY > y1 && mouseX < x2 && mouseY < y2;
}

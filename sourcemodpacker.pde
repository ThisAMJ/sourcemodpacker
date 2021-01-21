import drop.*;
import java.nio.file.*;

boolean processing;
int progress;
int amt;

String dropped;
ArrayList<String> console;
SDrop drop;

void setup() {
  size(1280, 720);
  surface.setTitle("Source Mod Packer");

  console = new ArrayList<String>();

  processing = false;
  progress = 0;
  amt = 0;

  drop = new SDrop(this);
}

void draw() {
  background(0);
  while (console.size() >= 25) {
    console.remove(0);
  }
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

String[] alta(ArrayList<String> al) {
  String[] array = new String[al.size()];
  for (int i = 0; i < al.size(); i++) {
    array[i] = al.get(i);
  }
  return array;
}

void dropArrow(float x, float y) {
  triangle(x - 30, y, x + 30, y, x, y + 15);
}

void fileCopy(String from, String to) {
  if (!from.equals(to)) {
    if (new File(from).exists()) {
      if (new File(to).exists()) {
        console.add("WARN: " + shortenPath(to) + " already exists, overwriting");
        new File(to).delete();
      } else {
        new File(to.substring(0, to.lastIndexOf('\\'))).mkdirs();
      }
      console.add("INFO: Copying " + shortenPath(from) + " to " + shortenPath(to));
      Path fromPath = Paths.get(from);
      Path toPath = Paths.get(to);
      try {
        Files.copy(fromPath, toPath);
      } 
      catch (IOException e) {
        console.add("ERRR: Copy failed!!");
      }
    } else {
      console.add("ERRR: " + shortenPath(from) + "doesn't exist!");
    }
  }
}

void fileMove(String from, String to) {
  if (!from.equals(to)) {
    if (new File(from).exists()) {
      if (new File(to).exists()) {
        console.add("WARN: "+ shortenPath(to) + " already exists, overwriting");
        new File(to).delete();
      } else {
        new File(to.substring(0, to.lastIndexOf('\\'))).mkdirs();
      }
      new File(from).renameTo(new File(to));
    } else {
      console.add("ERRR: " + shortenPath(from) + "does not exist!");
    }
  }
}

// Function to get a list of all files in a directory and all subdirectories
ArrayList<File> listFilesRecursive(String d, Boolean iD, Boolean iF) {
  ArrayList<File> fL = new ArrayList<File>(); 
  rDir(fL, d, iD, iF);
  return fL;
}

// Recursive function to traverse subdirectories
void rDir(ArrayList<File> a, String d, Boolean iD, Boolean iF) {
  File f = new File(d);
  if (f.isDirectory()) {
    if (iD) {
      a.add(f);
    } 
    File[] sf = f.listFiles();
    for (int i = 0; i < sf.length; i++) {
      rDir(a, sf[i].toString(), iD, iF);
    }
  } else if (iF) {
    a.add(f);
  }
}

boolean deleteDirectory(File dir) {
  File[] allContents = dir.listFiles();
  if (allContents != null) {
    for (File file : allContents) {
      deleteDirectory(file);
    }
  }
  return dir.delete();
}

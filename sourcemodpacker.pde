import drop.*;
import java.nio.file.*;

boolean processing;
int progress;
int amt;
float textSize;
float consoleVis;

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
  setTextSize(10);
  for (int i = 0; i < console.size(); i++) {
    text(console.get(i), 0, i * textSize);
  }
  setTextSize(30);
  if (processing) {
    rectMode(CENTER);
    fill(255);
    rect(width/2, height - 30, width - 2 * 25, 25);
    fill(0, 255, 0);
    rectMode(CORNER);
    rect(5, height - 42.5, (progress / float(amt)) * (width - 2 * 25), 25);
  } else {
    dropArrow(width / 2.0, height / 2.0 + sin(frameCount / 15.0) * 15);
    textAlign(CENTER, TOP);
    text("Drop a folder or texture\nto convert it!", width / 2.0, height / 2.0 + 30);
  }
  textAlign(RIGHT,TOP);
  setTextSize(13);
  text("Credits\nVTFCmd: Neil Jedrzejewski & Ryan Gregg\nVTEX / VPK: Valve Corporation", width, 0);
}

void setTextSize(int sz) {
  textSize = sz;
  textSize(sz);
}

void dropArrow(float x, float y) {
  triangle(x - 30, y, x + 30, y, x, y + 15);
}

void vtfify(String path) {
  if (new File(path.substring(0, path.length() - 7) + ".txt").exists()) {
    console.add("INFO: Skipping " + shortenPath(path) + " as it is part of an animated texture.");
  } else {
    File f = new File(path);
    if (f.exists()) {
      String out = newPath(path.substring(0, path.lastIndexOf('\\')));
      new File(out).mkdirs();
      if (f.toString().endsWith(".txt")) {
        run(new String[]{dataPath("vtex"), "-quiet", "-game", dataPath(""), "-outdir", out, path});
        new File(newPath(path).replace(".txt", ".pwl.vtf")).delete(); //vtex creates .pwl.vtf files, we dont want those
      } else {
        run(new String[]{dataPath("vtfcmd"), "-silent", "-file", path, "-output", out});
      }
    }
  }
}

void dropEvent(DropEvent theDropEvent) {
  processing = true;
  console = new ArrayList<String>();
  dropped = theDropEvent.toString();
  if (theDropEvent.isFile()) {
    if (theDropEvent.file().isDirectory()) {
      ArrayList<File> files;
      if (new File(dropped + "\\modifications").exists()) {
        files = listFilesRecursive(dropped + "\\modifications", false, true);
      } else {
        files = listFilesRecursive(dropped, false, true);
      }
      amt = files.size();
      progress = 0;
      for (File f : files) {
        progress++;
        if (isImg(f.toString())) {
          vtfify(f.toString());
        } else {
          fileCopy(f.toString(), newPath(f.toString()));
        }
      }
      console.add("INFO: Processed " + amt + " files!");
      if (new File(dropped + "\\new").exists()) {
        File vpk = new File(dropped + "\\new.vpk");
        File finalVPK = new File(dropped + "\\pak01_dir.vpk");
        if (vpk.exists() || finalVPK.exists()) {
          console.add("WARN: VPK already exists, overwriting");
          if (vpk.exists()) {
            vpk.delete();
          }
          if (finalVPK.exists()) {
            finalVPK.delete();
          }
        }
        run(new String[]{dataPath("vpk"), dropped + "\\new"});
        if (vpk.exists()) {
          vpk.renameTo(finalVPK);
        }
      }
    } else if (isImg(dropped)) {
      File anim = new File(dropped.substring(0, dropped.length() - 7) + ".txt");
      if (anim.exists()) {
        vtfify(anim.toString());
      } else {
        vtfify(dropped);
      }
    }
  }
  processing = false;
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

boolean isImg(String p) {
  return (p.endsWith(".bmp") ||
    p.endsWith(".jpg") ||
    p.endsWith(".png") ||
    p.endsWith(".tga") ||
    p.endsWith(".txt"));
}

void run(String[] args) {
  String stat = "INFO: Executing ";
  stat += args[0].replace(dataPath("") + '\\', "") + "    "; //remove path from exec
  for (int i = 1; i < args.length; i++) {
    stat += shortenPath(args[i]) + "    "; //appends argument with large spacing
  }
  stat = stat.trim();
  console.add(stat);
  try {
    Process p = exec(args);
    try {
      p.waitFor();
    } 
    catch (InterruptedException e) {
      console.add("ERRR: Process interrupted!");
    }
  } 
  catch (RuntimeException e) {
    console.add("ERRR: Process does not exist!");
  }
}

String shortenPath(String path) {
  String temp = path.replace(dataPath(""), "HOME");
  if (new File(dropped).isDirectory()) {
    //if you drop a folder, it will shorten the path until after the folder
    temp = temp.replace(dropped, "");
  } else if (temp.indexOf("modifications") > -1) {
    //if you drop a file that is part of a texture pack, then it will shorten the path until after the texture pack root
    temp = temp.substring(temp.indexOf("modifications") + 14);
  } else if (temp.indexOf("new") > -1) {
    //destination argument is also shortened the same way
    temp = temp.substring(temp.indexOf("new") + 4);
  } else {
    //if you drop a file that is not part of a texture pack, it will just give \filename.
    temp = temp.replace(dropped.substring(0, dropped.lastIndexOf('\\')), "");
  }
  return temp;
}

String newPath(String oldPath) {
  return oldPath.replace("modifications", "new");
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

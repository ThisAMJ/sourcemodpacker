
void vtfify(String path) {
  if (new File(path.substring(0, path.length() - 7) + ".txt").exists()) {
    console.add("INFO: Skipping " + shortenPath(path) + " as it is part of an animated texture.");
  } else {
    File f = new File(path);
    if (f.exists()) {
      String out = newPath(path.substring(0, path.lastIndexOf('\\')));
      new File(out).mkdirs(); // vtex/vtfcmd don't create requisite directories
      if (f.toString().endsWith(".txt")) {
        // use vtex to convert animated textures
        run(new String[]{dataPath("vtex"), "-quiet", "-game", dataPath(""), "-outdir", out, path});
        new File(newPath(path).replace(".txt", ".pwl.vtf")).delete(); //vtex creates .pwl.vtf files, we dont want those
      } else {
        // use vtfcmd to convert regular textures
        run(new String[]{dataPath("vtfcmd"), "-silent", "-file", path, "-output", out});
      }
    }
  }
}

void dropEvent(DropEvent theDropEvent) {
  if (!settings) {
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

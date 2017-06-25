# convert an ATF file into a clean CSV file
with open(R"C:\Users\swharden\Documents\temp\test.atf") as f:
    raw=f.readlines()[11:]
out=""
for i,line in enumerate(raw):
    out+="%.04f\n"%float(line.split('\t')[1])
with open("out.csv",'w') as f:
    f.write(out)
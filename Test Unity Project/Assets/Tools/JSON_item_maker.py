import json

class item():
    def __init__(self, name="default name", description="nulldescription", cost="1",weight="1"):
        self.name = name
        self.description = description
        self.cost = cost
        self.weight = weight

class itemCollection():
    def __init__(self):
        self.contents = {}
        self.lastID = 0

    def newItem(self, item):
        self.contents[lastID] = item
        self.lastID += 1

    
def writeFile(filename, data):
    f = open(filename, 'w')
    f.write(data)
    f.close()

def parseFile(inFile):
    f = open(inFile,'r')
    data = f.read()
    f.close()

    outJSON = {'items':{}}

    data = data.split('\n')

    #removes file comments
    for item in data:
        if "//" in item:
            data.remove(data[data.index(item)])

    header = "" 
        
    for element in range(len(data)):
        if "#" in data[element]:
            data[element] = data[element][1:]
            header = data[element]
            header = header.split(',')
            
        data[element] = data[element].split(',')
        
        tempDict = {}
        for attribute in header:
            if(attribute != data[element][header.index(attribute)]): #check so self-fulfilling stats aren't added
                tempDict[attribute] = data[element][header.index(attribute)]
        '''
        tempDict["description"] = data[element][1]
        tempDict["cost"] = data[element][2]
        tempDict["weight"] = data[element][3]
        '''

        if data[element][0] != header[0]: #check so empty items aren't added
            outJSON['items'][data[element][0]] = data[element][0]
            outJSON['items'][data[element][0]] = tempDict


    return json.dumps(outJSON)

stuff = parseFile('itemlist.txt')

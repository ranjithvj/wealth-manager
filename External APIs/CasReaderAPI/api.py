import flask
import casparser
from flask import jsonify
from flask import request


app = flask.Flask(__name__)
app.config["DEBUG"] = True


@app.route('/', methods=['GET'])
def home():
    return "<h1>This is CAS PDF Parser API</h1><p>POST to api/parsecasfile with the file location and password to get parsed JSON response</p>"

@app.route('/parsecasfile', methods=['GET'])
def parsecas():
    #"C:/Users/ranjith.vijayabaskar/Desktop/My Projects/Pdfreader/casreaderapi/CAMS.pdf"
    query_parameters = request.args
    fileLocation = query_parameters.get('fileLocation')
    filePassword = query_parameters.get('filePassword')
    json_val = casparser.read_cas_pdf(fileLocation,filePassword, output="json")
    return json_val

app.run()
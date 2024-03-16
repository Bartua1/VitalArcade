# -*- coding: utf-8 -*-
from flask import Flask, render_template, request, jsonify, json

app = Flask(__name__)
app.config['TEMPLATES_AUTO_RELOAD'] = True

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/cv')
def cv():
    return render_template('cv.html')

if __name__ == '__main__':
    app.run(host="localhost", port=5001, debug=True)


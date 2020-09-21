import eyed3

audiofile = eyed3.load("01 Dreams for Sale (feat_ Dr_ Tingle Fingers).mp3")
print(audiofile.tag.header)
print(audiofile.tag.title)
print(audiofile.tag.artist)
print(audiofile.tag.album)
#print(audiofile.tag.year)
#print(audiofile.tag.comment)
#print(audiofile.tag.track)
#print(audiofile.tag.genre)
